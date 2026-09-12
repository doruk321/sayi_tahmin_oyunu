// İÇERİK SCRIPT'İ
// Bu dosya sayfayla doğrudan iletişim kurar.
// popup.js'den gelen mesajları dinler ve sayfayı değiştirir.

let readingModeActive = false;
let overlay = null;
let readerFontSize = 22;
let readerLineHeight = 1.8;

// Metin çıkarma sonucu önbelleklenir — aynı sayfa için tekrar tekrar çalışmaz
let _cachedText = null;
let _cachedUrl  = null;

chrome.runtime.onMessage.addListener((msg, sender, sendResponse) => {
  switch (msg.action) {
    case "getText":
      sendResponse({ text: getTextCached() });
      break;
    case "highlight":
      highlightParagraph(msg.index);
      break;
    case "toggleReadMode":
      msg.enabled ? enableReadMode() : disableReadMode();
      break;
    case "setFontSize":
      readerFontSize = parseInt(msg.value);
      applyReaderStyles();
      break;
    case "setLineHeight":
      readerLineHeight = parseFloat(msg.value);
      applyReaderStyles();
      break;
    case "toggleTheme":
      applyTheme(msg.isLight);
      break;
  }
  return true;
});

// METİN ÇIKARMA
// Aynı URL de tekrar çağrılırsa önbellekten döner DOM u tekrar taramaz
function getTextCached() {
  const url = location.href;
  if (_cachedUrl === url && _cachedText) return _cachedText;
  _cachedUrl  = url;
  _cachedText = extractText();
  return _cachedText;
}

function extractText() {
  // Önce makale/içerik bloğunu bulmaya çalış, yoksa body'yi kullan
  const selectors = [
    "article", "main", "[role='main']",
    ".content", ".post-content", ".entry-content",
    ".article-body", "#content", ".story-body"
  ];
  let container = null;
  for (const sel of selectors) {
    container = document.querySelector(sel);
    if (container) break;
  }
  if (!container) container = document.body;

  // Klonla ve gereksiz elementleri temizle
  const clone = container.cloneNode(true);
  clone.querySelectorAll(
    "script,style,nav,header,footer,aside,.ad,.advertisement,[class*='cookie'],[class*='popup'],[class*='banner']"
  ).forEach(el => el.remove());

  return Array.from(clone.querySelectorAll("p,h1,h2,h3,h4,li"))
    .map(el => el.textContent.trim())
    .filter(t => t.length > 20)
    .join("\n\n");
}

// OKUMA MODU 
function enableReadMode() {
  if (readingModeActive) return;
  readingModeActive = true;

  const rawText  = getTextCached();
  const paraList = rawText.split(/\n+/).map(s => s.trim()).filter(s => s.length > 20);

  // DocumentFragment kullanımı → daha hızlı DOM oluşturma
  const frag = document.createDocumentFragment();
  const parasDiv = document.createElement("div");
  parasDiv.id = "erisibilir-body";

  paraList.forEach((p, i) => {
    const el = document.createElement("p");
    el.className    = "reader-para";
    el.dataset.index = i;
    el.contentEditable = "true";
    el.textContent  = p; // escapeHtml'e gerek yok, textContent XSS-safe
    parasDiv.appendChild(el);
  });
  frag.appendChild(parasDiv);

  overlay = document.createElement("div");
  overlay.id = "erisibilir-overlay";

  const reader = document.createElement("div");
  reader.id = "erisibilir-reader";

  // Toolbar
  const toolbar = document.createElement("div");
  toolbar.id = "reader-toolbar";
  toolbar.innerHTML = `
    <span id="reader-title">📖 Okuma Modu</span>
    <div id="reader-toolbar-right">
      <span id="reader-edit-hint">✏️ Metne tıklayarak düzenleyebilirsin</span>
      <button id="erisibilir-close">✕ Kapat</button>
    </div>
  `;

  reader.appendChild(toolbar);
  reader.appendChild(frag);
  overlay.appendChild(reader);

  injectReaderStyle();
  document.body.appendChild(overlay);
  applyReaderStyles();

  document.getElementById("erisibilir-close").addEventListener("click", disableReadMode);
  document.addEventListener("keydown", handleEsc);
}

function handleEsc(e) {
  if (e.key === "Escape") disableReadMode();
}

function disableReadMode() {
  readingModeActive = false;
  overlay?.remove();
  overlay = null;
  document.getElementById("erisibilir-reader-css")?.remove();
  document.removeEventListener("keydown", handleEsc);
}

function injectReaderStyle() {
  if (document.getElementById("erisibilir-reader-css")) return;
  const style = document.createElement("style");
  style.id = "erisibilir-reader-css";
  style.textContent = `
    #erisibilir-overlay {
      position: fixed !important;
      inset: 0 !important;
      z-index: 2147483647 !important;
      background: #0d0f1a !important;
      overflow-y: auto !important;
      display: flex !important;
      justify-content: center !important;
    }
    #erisibilir-reader {
      width: 100% !important;
      max-width: 780px !important;
      min-height: 100vh !important;
      background: #0d0f1a !important;
      color: #dde1f9 !important;
      padding: 0 0 80px !important;
    }
    #reader-toolbar {
      position: sticky !important;
      top: 0 !important;
      background: #13162a !important;
      border-bottom: 1px solid #2e3354 !important;
      padding: 14px 32px !important;
      display: flex !important;
      align-items: center !important;
      justify-content: space-between !important;
      z-index: 10 !important;
      gap: 12px !important;
    }
    #reader-title {
      font-family: sans-serif !important;
      font-size: 14px !important;
      font-weight: 700 !important;
      color: #8b90b8 !important;
      white-space: nowrap !important;
    }
    #reader-toolbar-right {
      display: flex !important;
      align-items: center !important;
      gap: 16px !important;
    }
    #reader-edit-hint {
      font-family: sans-serif !important;
      font-size: 11px !important;
      color: #4f6ef7 !important;
      opacity: 0.8 !important;
    }
    #erisibilir-close {
      background: #1e2238 !important;
      color: #8b90b8 !important;
      border: 1px solid #2e3354 !important;
      border-radius: 8px !important;
      padding: 6px 16px !important;
      font-size: 12px !important;
      font-family: sans-serif !important;
      cursor: pointer !important;
      transition: background 0.2s !important;
    }
    #erisibilir-close:hover { background: #4f6ef7 !important; color: white !important; border-color: #4f6ef7 !important; }
    #erisibilir-body {
      padding: 48px 48px 0 !important;
      font-family: Georgia, 'Times New Roman', serif !important;
    }
    .reader-para {
      display: block !important;
      margin-bottom: 1.4em !important;
      color: #dde1f9 !important;
      font-family: Georgia, 'Times New Roman', serif !important;
      border-radius: 6px !important;
      padding: 6px 10px !important;
      outline: none !important;
      transition: background 0.15s !important;
      cursor: text !important;
    }
    .reader-para:hover  { background: rgba(79,110,247,0.07) !important; }
    .reader-para:focus  { background: rgba(79,110,247,0.13) !important; }
    .erisibilir-highlight {
      background: rgba(79,110,247,0.22) !important;
      border-left: 3px solid #4f6ef7 !important;
      padding-left: 8px !important;
    }
    @media (max-width: 600px) {
      #erisibilir-body { padding: 24px 20px 0 !important; }
      #reader-toolbar  { padding: 12px 16px !important; }
      #reader-edit-hint { display: none !important; }
    }
  `;
  document.head.appendChild(style);
}

function applyReaderStyles() {
  if (!overlay) return;
  const body = document.getElementById("erisibilir-body");
  if (!body) return;
  const fs = readerFontSize + "px";
  const lh = readerLineHeight;
  body.style.fontSize   = fs;
  body.style.lineHeight = lh;
  // querySelectorAll tek seferde hepsini güncelle
  body.querySelectorAll(".reader-para").forEach(p => {
    p.style.fontSize   = fs;
    p.style.lineHeight = lh;
  });
}

// ─── VURGULAMA ──────────────────────────────────────────────
let _highlightStyle = false;

function highlightParagraph(index) {
  if (readingModeActive && overlay) {
    const body = document.getElementById("erisibilir-body");
    if (!body) return;
    body.querySelectorAll(".erisibilir-highlight").forEach(el => el.classList.remove("erisibilir-highlight"));
    const target = body.querySelector(`.reader-para[data-index="${index}"]`);
    if (target) {
      target.classList.add("erisibilir-highlight");
      target.scrollIntoView({ behavior: "smooth", block: "center" });
    }
    return;
  }

  // Normal sayfa vurgulaması
  if (!_highlightStyle) {
    const style = document.createElement("style");
    style.id = "erisibilir-highlight-css";
    style.textContent = `
      .erisibilir-highlight {
        background: rgba(79,110,247,0.18) !important;
        border-left: 3px solid #4f6ef7 !important;
        padding-left: 8px !important;
        border-radius: 4px !important;
        transition: background 0.3s !important;
      }
    `;
    document.head.appendChild(style);
    _highlightStyle = true;
  }

  document.querySelectorAll(".erisibilir-highlight").forEach(el => el.classList.remove("erisibilir-highlight"));
  const paras = Array.from(document.querySelectorAll("p,h1,h2,h3,h4,li"))
    .filter(el => el.textContent.trim().length > 20);
  if (paras[index]) {
    paras[index].classList.add("erisibilir-highlight");
    paras[index].scrollIntoView({ behavior: "smooth", block: "center" });
  }
}

// ─── TEMA ────────────────────────────────────────────────────
function applyTheme(isLight) {
  document.body.style.filter = isLight ? "invert(1) hue-rotate(180deg)" : "";
}
