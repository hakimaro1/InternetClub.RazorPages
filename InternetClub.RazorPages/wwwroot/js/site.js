// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

function formatRuDateFromDigits(digits) {
    const d = digits.slice(0, 8);
    if (d.length <= 2) return d;
    if (d.length <= 4) return d.slice(0, 2) + "." + d.slice(2);
    return d.slice(0, 2) + "." + d.slice(2, 4) + "." + d.slice(4);
}

function applyRuDateMask(input) {
    const oldValue = input.value ?? "";
    const digits = oldValue.replace(/\D/g, "");
    input.value = formatRuDateFromDigits(digits);
}

document.addEventListener("input", (e) => {
    const el = e.target;
    if (!(el instanceof HTMLInputElement)) return;
    if (el.dataset.dateMask !== "ru-date") return;
    applyRuDateMask(el);
}, true);

document.addEventListener("paste", (e) => {
    const el = e.target;
    if (!(el instanceof HTMLInputElement)) return;
    if (el.dataset.dateMask !== "ru-date") return;
    setTimeout(() => applyRuDateMask(el), 0);
}, true);
