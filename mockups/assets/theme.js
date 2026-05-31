/* FormulaForge — Tailwind config + theme + icon helpers (load AFTER the Tailwind CDN, BEFORE page scripts) */

if (window.tailwind) {
  tailwind.config = {
    darkMode: 'class',
    theme: {
      extend: {
        fontFamily: {
          sans: ['Geist', 'ui-sans-serif', 'system-ui', '-apple-system', 'sans-serif'],
          mono: ['"Geist Mono"', 'ui-monospace', 'SFMono-Regular', 'monospace'],
        },
      },
    },
  };
}

/* Pre-paint theme is set inline in <head>; this just wires the toggle. */
function ffSetTheme(mode) {
  const root = document.documentElement;
  if (mode === 'dark') root.classList.add('dark');
  else root.classList.remove('dark');
  try { localStorage.setItem('ff-theme', mode); } catch (e) {}
  ffSyncThemeIcon();
}
function ffToggleTheme() {
  ffSetTheme(document.documentElement.classList.contains('dark') ? 'light' : 'dark');
}
function ffSyncThemeIcon() {
  const dark = document.documentElement.classList.contains('dark');
  document.querySelectorAll('[data-theme-icon]').forEach((el) => {
    el.setAttribute('data-lucide', dark ? 'sun' : 'moon');
  });
  if (window.lucide) lucide.createIcons();
}

function ffInitIcons() { if (window.lucide) lucide.createIcons(); }

document.addEventListener('DOMContentLoaded', () => {
  ffInitIcons();
  ffSyncThemeIcon();
});
