// ─── AYARLAR ────────────────────────────────────────────────
// ElevenLabs API anahtarın ve ses ID'lerin burada tanımlanır.
// ERGUN_VOICE_ID: ElevenLabs'ta klonladığın Ergun Kalbak sesinin ID'si.
// FALLBACK_VOICE_ID: Ergun sesi bulunamazsa kullanılacak yedek Türkçe ses.
const ELEVENLABS_API_KEY = "sk_2a828a8d039f3fe0e964ab825adcefd0f7ac5c30cb53a5e7";
const ERGUN_VOICE_ID     = "ErgunKalbak";       // Kendi klonladığın sesin ID'sini buraya yaz
const FALLBACK_VOICE_ID  = "pNInz6obpgDQGcFmaJgB"; // Yedek ses (Adam - çok dilli)
const ELEVENLABS_MODEL   = "eleven_multilingual_v2";

// ─── DURUM DEĞİŞKENLERİ ─────────────────────────────────────
// Uygulamanın anlık durumunu takip eden değişkenler
let audio       = null;   // Şu an çalan ses nesnesi
let isPlaying   = false;  // Ses çalıyor mu?
let isFetching  = false;  // API'den ses indiriliyor mu? (çift tıklamayı önler)
let readingMode = false;  // Okuma modu açık mı?
let paragraphs  = [];     // Sayfadan çekilen paragraf listesi
let currentPara = 0;      // Şu an okunan paragrafın sırası
let isLight     = false;  // Açık tema mı?
let readSpeed   = 1.0;    // Okuma hızı (0.5x – 2.0x)

// Ses önbelleği: aynı paragrafı tekrar API'den çekmemek için blob URL saklar
const audioCache = new Map(); // index → blob URL

// ─── ARAYÜZ ELEMENTLERİ ─────────────────────────────────────
const statusBox        = document.getElementById("statusBox");
const btnOku           = document.getElementById("btnOku");
const btnDur           = document.getElementById("btnDur");
const btnGeri          = document.getElementById("btnGeri");
const btnIleri         = document.getElementById("btnIleri");
const btnReadMode      = document.getElementById("btnReadMode");
const themeToggle      = document.getElementById("themeToggle");
const fontSizeSlider   = document.getElementById("fontSize");
const lineHeightSlider = document.getElementById("lineHeight");
const readSpeedSlider  = document.getElementById("readSpeed");
const fontSizeVal      = document.getElementById("fontSizeVal");
const lineHeightVal    = document.getElementById("lineHeightVal");
const readSpeedVal     = document.getElementById("readSpeedVal");

// ─── KAYITLI AYARLARI YÜKLE ─────────────────────────────────
// Kullanıcının önceki oturumda kaydettiği yazı boyutu, satır aralığı vs. yüklenir.
chrome.storage.local.get(["fontSize", "lineHeight", "isLight", "readSpeed"], (data) => {
  if (data.fontSize) {
    fontSizeSlider.value = data.fontSize;
    fontSizeVal.textContent = data.fontSize + "px";
  }
  if (data.lineHeight) {
    lineHeightSlider.value = data.lineHeight;
    lineHeightVal.textContent = (data.lineHeight / 10).toFixed(1);
  }
  if (data.readSpeed) {
    readSpeedSlider.value = data.readSpeed;
    readSpeed = data.readSpeed / 10;
    readSpeedVal.textContent = readSpeed.toFixed(1) + "x";
  }
  if (data.isLight) {
    isLight = data.isLight;
    if (isLight) document.body.classList.add("light");
  }
});

// ─── KAYDIRICILAR (SLIDERS) ──────────────────────────────────
fontSizeSlider.addEventListener("input", () => {
  const v = fontSizeSlider.value;
  fontSizeVal.textContent = v + "px";
  chrome.storage.local.set({ fontSize: v });
  sendToContent({ action: "setFontSize", value: v });
});

lineHeightSlider.addEventListener("input", () => {
  const v = lineHeightSlider.value;
  lineHeightVal.textContent = (v / 10).toFixed(1);
  chrome.storage.local.set({ lineHeight: v });
  sendToContent({ action: "setLineHeight", value: (v / 10).toFixed(1) });
});

readSpeedSlider.addEventListener("input", () => {
  const v = readSpeedSlider.value;
  readSpeed = v / 10;
  readSpeedVal.textContent = readSpeed.toFixed(1) + "x";
  chrome.storage.local.set({ readSpeed: v });
  // Ses şu an çalıyorsa hızı anında güncelle, beklemeden uygula
  if (audio) audio.playbackRate = readSpeed;
});

// ─── TEMA DEĞİŞTİR ──────────────────────────────────────────
themeToggle.addEventListener("click", () => {
  isLight = !isLight;
  document.body.classList.toggle("light", isLight);
  chrome.storage.local.set({ isLight });
  sendToContent({ action: "toggleTheme", isLight });
});

// ─── OKUMA MODU ─────────────────────────────────────────────
btnReadMode.addEventListener("click", () => {
  readingMode = !readingMode;
  sendToContent({ action: "toggleReadMode", enabled: readingMode });
  btnReadMode.classList.toggle("on", readingMode);
  setStatus(readingMode ? "Okuma modu açık" : "Okuma modu kapalı", readingMode ? "playing" : "");
});

// ─── OKU BUTONU ─────────────────────────────────────────────
// Sayfadaki metni çekip paragraf paragraf okumaya başlar.
btnOku.addEventListener("click", async () => {
  // Zaten yükleniyorsa ya da çalıyorsa tekrar başlatma
  if (isPlaying || isFetching) return;

  const text = await getPageText();
  if (!text) { setStatus("Sayfada okunacak metin bulunamadı", "error"); return; }

  const newParas = text.split(/\n+/).map(s => s.trim()).filter(s => s.length > 20);
  if (!newParas.length) { setStatus("Okunacak içerik yok", "error"); return; }

  // Yeni sayfaysa önbelleği temizle
  if (JSON.stringify(newParas) !== JSON.stringify(paragraphs)) {
    clearAudioCache();
    paragraphs = newParas;
    currentPara = 0;
  }

  btnOku.classList.add("active");
  await playParagraph(currentPara);
});

// ─── DUR BUTONU ─────────────────────────────────────────────
// Sesi hemen durdurur ama paragraf konumunu korur.
// Tekrar "Oku" ya basınca kaldığı yerden devam edebilmek için tasarlandı.
btnDur.addEventListener("click", () => {
  stopAudio();
  isFetching = false;
  btnOku.classList.remove("active");
  setStatus(`⏸ Durduruldu — ${currentPara + 1}. paragraf`, "stopped");
  updateNavButtons();
});

// ─── GERİ BUTONU ─────────────────────────────────────────────
// Bir önceki paragrafa geçer ve oradan okumaya devam eder.
// Zaten yükleniyorsa (isFetching) tıklamayı görmezden gelir.
btnGeri.addEventListener("click", async () => {
  if (isFetching) return;
  if (currentPara <= 0) {
    setStatus("En başta bulunuyorsun", "stopped");
    return;
  }
  stopAudio();
  currentPara--;
  updateNavButtons();
  await playParagraph(currentPara);
});

// ─── İLERİ BUTONU ───────────────────────────────────────────
// Bir sonraki paragrafa atlar ve oradan okumaya devam eder.
// Paragraf listesi yoksa önce sayfayı çeker.
btnIleri.addEventListener("click", async () => {
  if (isFetching) return;

  // Henüz okuma başlamamışsa, sayfayı önce çek
  if (!paragraphs.length) {
    const text = await getPageText();
    if (!text) { setStatus("Sayfada metin bulunamadı", "error"); return; }
    paragraphs = text.split(/\n+/).map(s => s.trim()).filter(s => s.length > 20);
    currentPara = 0;
  }

  if (currentPara >= paragraphs.length - 1) {
    setStatus("En sonda bulunuyorsun", "stopped");
    return;
  }
  stopAudio();
  currentPara++;
  updateNavButtons();
  await playParagraph(currentPara);
});

// ─── PARAGRAF ÇALMA ─────────────────────────────────────────
async function playParagraph(index) {
  if (index >= paragraphs.length) {
    setStatus("✓ Tüm sayfa okundu!", "playing");
    btnOku.classList.remove("active");
    isPlaying  = false;
    isFetching = false;
    updateNavButtons();
    return;
  }

  const text = paragraphs[index];
  isFetching = true;
  setStatus(`Yükleniyor<span class='dot1'>.</span><span class='dot2'>.</span><span class='dot3'>.</span> (${index + 1}/${paragraphs.length})`, "loading");
  updateNavButtons();

  try {
    // Önbellekte varsa API'ye gitme, direk kullan
    let url = audioCache.get(index);
    if (!url) {
      const blob = await fetchTTS(text);
      url = URL.createObjectURL(blob);
      audioCache.set(index, url);
    }

    // Bu sürede kullanıcı "Dur" ya da gezinme butonuna basmış olabilir — iptal kontrolü
    if (!isFetching) return;

    stopAudio();
    audio = new Audio(url);
    audio.playbackRate = readSpeed;
    isPlaying  = true;
    isFetching = false;

    setStatus(`▶ Okunuyor (${index + 1}/${paragraphs.length})`, "playing");
    highlightParagraph(index);
    updateNavButtons();

    // Bir sonraki paragrafı arka planda önceden indir (varsa)
    prefetchNext(index + 1);

    audio.play();
    audio.onended = async () => {
      isPlaying = false;
      currentPara = index + 1;
      updateNavButtons();
      await playParagraph(currentPara);
    };
    audio.onerror = () => {
      setStatus("Ses çalınamadı — tekrar dene", "error");
      isPlaying  = false;
      isFetching = false;
      btnOku.classList.remove("active");
      updateNavButtons();
    };
  } catch (err) {
    isFetching = false;
    isPlaying  = false;
    setStatus("Hata: " + err.message, "error");
    btnOku.classList.remove("active");
    updateNavButtons();
  }
}

// ─── ÖN YÜKLEME (PREFETCH) ──────────────────────────────────
// Şu an okunan paragraf çalarken bir sonrakini sessizce indirir.
// Böylece "İleri" tuşuna basıldığında bekleme süresi neredeyse sıfır olur.
async function prefetchNext(index) {
  if (index >= paragraphs.length) return;
  if (audioCache.has(index)) return; // Zaten var
  try {
    const blob = await fetchTTS(paragraphs[index]);
    audioCache.set(index, URL.createObjectURL(blob));
  } catch (_) {
    // Ön yükleme başarısız olursa sorun değil, ana akışta tekrar denenecek
  }
}

// ─── ÖNBELLEK TEMİZLE ───────────────────────────────────────
function clearAudioCache() {
  // Blob URL'lerini bellekten serbest bırak
  audioCache.forEach(url => URL.revokeObjectURL(url));
  audioCache.clear();
}

// ─── ELEVENLABS TTS API ─────────────────────────────────────
// Önce Ergun Kalbak sesini dener. Bulunamazsa yedek sese geçer.
async function fetchTTS(text) {
  const voiceIds = [ERGUN_VOICE_ID, FALLBACK_VOICE_ID];
  for (const voiceId of voiceIds) {
    try {
      const res = await fetch(
        `https://api.elevenlabs.io/v1/text-to-speech/${voiceId}`,
        {
          method: "POST",
          headers: {
            "xi-api-key": ELEVENLABS_API_KEY,
            "Content-Type": "application/json",
          },
          body: JSON.stringify({
            text,
            model_id: ELEVENLABS_MODEL,
            voice_settings: {
              stability: 0.55,
              similarity_boost: 0.80,
              style: 0.25,
              use_speaker_boost: true
            }
          })
        }
      );
      if (!res.ok) { console.warn(`Ses ID bulunamadı: ${voiceId}`); continue; }
      return await res.blob();
    } catch (e) {
      console.warn(`Ses hatası (${voiceId}):`, e);
    }
  }
  throw new Error("API'ye ulaşılamadı. API anahtarını ve ElevenLabs kredinizi kontrol edin.");
}

// ─── YARDIMCI FONKSİYONLAR ──────────────────────────────────

// Sesi durdurur ve audio nesnesini temizler
function stopAudio() {
  if (audio) {
    audio.onended = null; // Sesi durdurduğumuzda bir sonraki paragrafa geçmemeli
    audio.onerror = null;
    audio.pause();
    audio.src = "";
    audio = null;
  }
  isPlaying = false;
}

// Durum kutusunu günceller
function setStatus(msg, cls = "") {
  statusBox.innerHTML = msg;
  statusBox.className = "status-box " + cls;
}

// Geri/İleri butonlarını duruma göre aktif/pasif yapar
function updateNavButtons() {
  btnGeri.classList.toggle("active", currentPara > 0 && !isFetching);
  btnIleri.classList.toggle("active", paragraphs.length > 0 && currentPara < paragraphs.length - 1 && !isFetching);
}

// Sayfadaki metni content script aracılığıyla çeker
async function getPageText() {
  return new Promise((resolve) => {
    chrome.tabs.query({ active: true, currentWindow: true }, (tabs) => {
      if (!tabs[0]) { resolve(""); return; }
      chrome.tabs.sendMessage(tabs[0].id, { action: "getText" }, (resp) => {
        resolve(resp?.text || "");
      });
    });
  });
}

// Şu an okunan paragrafı sayfada vurgular ve görünür hale getirir
function highlightParagraph(index) {
  chrome.tabs.query({ active: true, currentWindow: true }, (tabs) => {
    if (!tabs[0]) return;
    chrome.tabs.sendMessage(tabs[0].id, { action: "highlight", index });
  });
}

// Content script'e mesaj gönderir (okuma modu, tema, yazı boyutu vb.)
function sendToContent(msg) {
  chrome.tabs.query({ active: true, currentWindow: true }, (tabs) => {
    if (!tabs[0]) return;
    chrome.tabs.sendMessage(tabs[0].id, msg, () => {
      if (chrome.runtime.lastError) { /* bağlantı yoksa sessizce geç */ }
    });
  });
}
