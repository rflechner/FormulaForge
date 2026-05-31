/* FormulaForge — sample data + table/scalar/code renderers */

const FF_MONTHS = (() => {
  const m = ['janv.', 'févr.', 'mars', 'avr.', 'mai', 'juin', 'juil.', 'août', 'sept.', 'oct.', 'nov.', 'déc.'];
  const out = [];
  for (const y of [2024, 2025]) for (const mm of m) out.push(`${mm} ${y}`);
  return out;
})();

const FF_RATE = [0.30,0.48,0.72,0.88,0.08,0.63,0.58,0.65,0.13,0.74,0.67,0.08];
const FF_BAL  = [100,200,300,400,500,600,700,800,900,1000,1100,1200];

const FF = {
  balance:    [...FF_BAL, ...FF_BAL],
  change_rate:[...FF_RATE, ...FF_RATE],
};
FF.balance_euro = FF.balance.map((v, i) => v * FF.change_rate[i]);
FF.balance_ma3  = FF.balance.map((_, i) => {
  const s = FF.balance_euro.slice(Math.max(0, i - 2), i + 1);
  return s.reduce((a, b) => a + b, 0) / s.length;
});

/* ---------- number formatting (fr-CH style: space thousands, comma decimals) ---------- */
function ffNum(v, dec = 2) {
  return new Intl.NumberFormat('fr-FR', { minimumFractionDigits: dec, maximumFractionDigits: dec }).format(v);
}

/* ---------- source chips ---------- */
const FF_SOURCES = {
  api:  { label: 'API',  icon: 'webhook',     cls: 'text-sky-700 bg-sky-50 ring-sky-600/20 dark:text-sky-300 dark:bg-sky-500/10 dark:ring-sky-400/20' },
  db:   { label: 'DB',   icon: 'database',    cls: 'text-violet-700 bg-violet-50 ring-violet-600/20 dark:text-violet-300 dark:bg-violet-500/10 dark:ring-violet-400/20' },
  csv:  { label: 'CSV',  icon: 'file-spreadsheet', cls: 'text-emerald-700 bg-emerald-50 ring-emerald-600/20 dark:text-emerald-300 dark:bg-emerald-500/10 dark:ring-emerald-400/20' },
  push: { label: 'PUSH', icon: 'radio',       cls: 'text-amber-700 bg-amber-50 ring-amber-600/20 dark:text-amber-300 dark:bg-amber-500/10 dark:ring-amber-400/20' },
  calc: { label: 'CALC', icon: 'function-square', cls: 'text-orange-700 bg-orange-50 ring-orange-600/20 dark:text-orange-300 dark:bg-orange-500/10 dark:ring-orange-400/20' },
};
function ffChip(kind) {
  const s = FF_SOURCES[kind] || FF_SOURCES.calc;
  return `<span class="inline-flex items-center gap-1 rounded px-1.5 py-0.5 text-[10px] font-semibold tracking-wide ring-1 ring-inset ${s.cls}">
    <i data-lucide="${s.icon}" class="h-3 w-3"></i>${s.label}</span>`;
}

/* ---------- dense series table ---------- */
/* rows: [{ name, source, values, computed, dec }] */
function ffSeriesTable(rows, opts = {}) {
  const dec = opts.dec ?? 2;
  const head = FF_MONTHS.map((m) => `<th class="px-3 py-2 text-right font-medium text-stone-500 dark:text-stone-400 bg-stone-50 dark:bg-stone-900/80 border-b border-stone-200 dark:border-stone-800">${m}</th>`).join('');
  const body = rows.map((r) => {
    const cells = r.values.map((v) => {
      const empty = v === null || v === undefined;
      return `<td class="px-3 py-2 text-right tnum text-stone-700 dark:text-stone-300 border-b border-stone-100 dark:border-stone-800/70 ${empty ? 'text-stone-300 dark:text-stone-600' : ''}">${empty ? '—' : ffNum(v, r.dec ?? dec)}</td>`;
    }).join('');
    return `<tr class="group hover:bg-orange-50/40 dark:hover:bg-stone-800/40">
      <th class="stick-l px-3 py-2 text-left bg-white dark:bg-stone-900 group-hover:bg-orange-50/40 dark:group-hover:bg-stone-800/40 border-b border-stone-100 dark:border-stone-800/70 border-r border-r-stone-200 dark:border-r-stone-800">
        <div class="flex items-center gap-2">
          ${ffChip(r.source)}
          <span class="font-mono text-[13px] text-stone-800 dark:text-stone-200">${r.name}</span>
        </div>
      </th>${cells}</tr>`;
  }).join('');
  return `<div class="ff-scroll overflow-x-auto rounded-lg border border-stone-200 dark:border-stone-800">
    <table class="ff-table w-full text-[13px]">
      <thead><tr>
        <th class="stick-l px-3 py-2 text-left font-semibold text-stone-500 dark:text-stone-400 bg-stone-50 dark:bg-stone-900/80 border-b border-stone-200 dark:border-stone-800 border-r border-r-stone-200 dark:border-r-stone-800">Variable</th>
        ${head}
      </tr></thead>
      <tbody>${body}</tbody>
    </table>
  </div>`;
}

/* ---------- scalar cards ---------- */
/* items: [{ name, value, source, unit }] */
function ffScalars(items) {
  return items.map((it) => `
    <div class="group relative min-w-[150px] rounded-lg border border-stone-200 dark:border-stone-800 bg-white dark:bg-stone-900 px-3.5 py-3">
      <div class="flex items-center justify-between gap-2">
        <span class="font-mono text-xs text-stone-500 dark:text-stone-400 truncate">${it.name}</span>
        ${ffChip(it.source)}
      </div>
      <div class="mt-1 flex items-baseline gap-1">
        <span class="tnum text-[22px] font-semibold leading-none text-stone-900 dark:text-stone-100">${it.value}</span>
        ${it.unit ? `<span class="text-xs text-stone-400">${it.unit}</span>` : ''}
      </div>
    </div>`).join('');
}

/* ---------- code highlighter for the custom language ---------- */
function ffHighlight(line) {
  const c = line.indexOf('#');
  let code = line, comment = '';
  if (c !== -1) { comment = line.slice(c); code = line.slice(0, c); }
  const esc = (s) => s.replace(/&/g, '&amp;').replace(/</g, '&lt;').replace(/>/g, '&gt;');
  const re = /("(?:[^"\\]|\\.)*")|(\b(?:mean|sum|min|max|cumsum|shift|lag|abs|round|pct_change|fill)\b(?=\s*\())|(\b\d+\.?\d*\b)|(\b(?:window|by|mode)\b(?=\s*:))|([=+\-*/%])/g;
  let out = '', last = 0, m;
  while ((m = re.exec(code))) {
    out += esc(code.slice(last, m.index));
    if (m[1]) out += `<span class="tok-str">${esc(m[1])}</span>`;
    else if (m[2]) out += `<span class="tok-fn">${esc(m[2])}</span>`;
    else if (m[3]) out += `<span class="tok-num">${esc(m[3])}</span>`;
    else if (m[4]) out += `<span class="tok-arg">${esc(m[4])}</span>`;
    else if (m[5]) out += `<span class="tok-op">${esc(m[5])}</span>`;
    last = re.lastIndex;
  }
  out += esc(code.slice(last));
  if (comment) out += `<span class="tok-comment">${esc(comment)}</span>`;
  return out || '&nbsp;';
}
function ffRenderCode(targetSel, code) {
  const lines = code.replace(/\n$/, '').split('\n');
  const gutter = lines.map((_, i) => `<div class="code-line text-right pr-3 text-stone-400 dark:text-stone-600 select-none">${i + 1}</div>`).join('');
  const body = lines.map((l, i) => `<div class="code-line ${i === 1 ? 'active' : ''}">${ffHighlight(l)}</div>`).join('');
  const host = document.querySelector(targetSel);
  if (host) host.querySelector('[data-gutter]').innerHTML = gutter,
            host.querySelector('[data-code]').innerHTML = body;
}
