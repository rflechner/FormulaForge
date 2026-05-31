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

// Re-init Lucide icons after every Blazor SignalR DOM patch
let _lucideTimer;
const _lucideObserver = new MutationObserver(() => {
    clearTimeout(_lucideTimer);
    _lucideTimer = setTimeout(() => {
        if (window.lucide) lucide.createIcons();
    }, 60);
});

document.addEventListener('DOMContentLoaded', () => {
    _lucideObserver.observe(document.body, { childList: true, subtree: true });
    if (window.lucide) { lucide.createIcons(); ffSyncThemeIcon(); }
});
