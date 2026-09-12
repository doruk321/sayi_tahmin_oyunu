# 📖 Erişilebilir Okuyucu – Chrome Eklentisi

Web sayfalarını Ergun Kalbak sesiyle (ElevenLabs API) sesli okuyan bir Chrome eklentisi.

---

## 📁 Klasör Yapısı

```
erisibilir-okuyucu/
├── manifest.json       ← Eklentinin kimlik kartı, izinler burada tanımlanır
├── popup.html          ← Eklenti ikonuna tıklayınca açılan arayüz
├── popup.js            ← Arayüzün mantığı (okuma, durdurma, gezinme vb.)
├── content.js          ← Sayfayla doğrudan iletişim kuran script
├── README.md           ← Bu dosya
└── icons/
    ├── icon16.png      ← Tarayıcı sekmesi için küçük ikon
    ├── icon48.png      ← Eklentiler listesi için orta boy ikon
    └── icon128.png     ← Chrome Web Store için büyük ikon
```

---

## 🎤 Ergun Kalbak Sesini Nasıl Ayarlarsın?

### Seçenek 1 – Kendi Klonladığın Ses (Önerilen)

ElevenLabs'ta Ergun Kalbak sesini zaten klonladıysan şu adımları izle:

1. [elevenlabs.io](https://elevenlabs.io) adresine gir
2. Sol menüden **"Voices"** → kendi oluşturduğun sese tıkla
3. Tarayıcı adres çubuğundaki URL'ye bak — sonundaki uzun karakter dizisi senin ses ID'ndir
   - Örnek URL: `https://elevenlabs.io/app/voice-lab/abc123xyz`
   - Ses ID: `abc123xyz`
4. `popup.js` dosyasını aç ve şu satırı güncelle:
   ```js
   const ERGUN_VOICE_ID = "abc123xyz"; 
   ```

### Seçenek 2 – Hazır Türkçe Ses (Yedek)

Klonlanmış sesin yoksa eklenti otomatik olarak **Adam** adlı sese geçer.
Daha iyi Türkçe ses istiyorsan ElevenLabs Voice Library'den şunlara bakabilirsin:

- **Serdar** – doğal Türkçe erkek sesi
- **Mehmet** – profesyonel Türkçe ses

Ses ID bulmak için:
1. [elevenlabs.io/voice-library](https://elevenlabs.io/voice-library) sayfasını aç
2. Dil filtresi olarak **Turkish** seç
3. Beğendiğin sesin üstüne tıkla → adres çubuğundan ID'yi kopyala
4. `popup.js` içindeki `FALLBACK_VOICE_ID` değişkenini güncelle

---

## 🚀 Eklentiyi Chrome'a Yükleme

### Adım 1 – Dosyaları Hazırla

İndirdiğin zip'i bir klasöre çıkar. Klasör yapısı yukarıdaki gibi olmalı.
`icons/` klasöründe 3 boyutta PNG ikonun olduğundan emin ol.

### Adım 2 – Chrome'a Yükle

1. Chrome'u aç
2. Adres çubuğuna `chrome://extensions/` yaz ve Enter'a bas
3. Sağ üst köşedeki **"Geliştirici modu"** anahtarını aç (toggle)
4. Sol üstte beliren **"Paketlenmemiş öğe yükle"** butonuna tıkla
5. Az önce çıkardığın `erisibilir-okuyucu/` klasörünü seç
6. Eklenti listeye eklendi ✅

### Adım 3 – Kullan

1. Herhangi bir haber veya blog sayfasına git
2. Sağ üstteki eklenti ikonuna tıkla
3. **"Oku"** butonuna bas — sayfa paragraf paragraf okunmaya başlar
4. **"Geri"** ve **"İleri"** ile paragraflar arasında gezinebilirsin
5. **"Dur"** ile istediğin an durdurabilirsin; tekrar "Oku"ya basınca kaldığı yerden devam eder

---

## ⚙️ Özellikler

| Özellik | Açıklama |
|---|---|
| 🎤 Sesli okuma | ElevenLabs ile gerçekçi Türkçe ses |
| ⏮ Geri / İleri | Paragraf paragraf ileri-geri geç; yüklenirken çift tıklamaya karşı koruma var |
| ⏸ Durdur | Okumayı durdurur, paragraf konumunu korur |
| ▶ Devam | "Oku"ya tekrar basınca kaldığı yerden devam eder |
| ⚡ Ön yükleme | Bir sonraki paragraf arka planda sessizce indirilir; "İleri" tuşuna basınca bekleme süresi neredeyse sıfır |
| 📖 Okuma Modu | Temiz, dikkat dağıtmayan okuma ekranı |
| 🔤 Yazı boyutu | 14px–36px arasında ayarlanabilir |
| ↕ Satır aralığı | 1.0–2.5 arasında ayarlanabilir |
| 🌙 Tema | Koyu / Açık tema |
| 💾 Ayar Kaydı | Yazı boyutu, satır aralığı, hız ayarları bir sonraki açılışta da korunur |

---

## 🔧 Sık Karşılaşılan Sorunlar

**"API'ye ulaşılamadı" hatası alıyorum:**
- `popup.js` içindeki `ELEVENLABS_API_KEY` değerinin doğru olduğunu kontrol et
- ElevenLabs hesabında yeterli kredi kaldığını kontrol et (ücretsiz planda aylık ~10.000 karakter)
- Voice ID'nin gerçekten var olduğunu ElevenLabs panelinden doğrula

**Eklenti sayfada hiçbir şey yapmıyor:**
- `chrome://extensions/` sayfasından eklentiyi kaldır ve tekrar yükle
- Sayfayı F5 ile yenile, sonra eklentiyi tekrar kullan
- Bazı sayfalar (chrome:// adresleri gibi) güvenlik nedeniyle eklentilere izin vermez

---
## 💡 Kullanım İpuçları

- Uzun makaleleri okurken **Okuma Modunu** açmak daha konforlu bir deneyim sağlar
- Hızı **1.2x–1.5x** arasına almak hem anlaşılır hem de daha hızlı okuma sağlar
- "Dur" butonu paragraf pozisyonunu unutmaz; daha sonra kaldığın yerden devam edebilirsin
