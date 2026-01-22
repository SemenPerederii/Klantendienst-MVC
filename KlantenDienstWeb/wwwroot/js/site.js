document.addEventListener('pointerdown', function (e) {
    const menu = e.target.closest('.dropdown-menu');
    if (menu) e.stopPropagation();
}, true);

document.addEventListener('click', function (e) {
    const menu = e.target.closest('.dropdown-menu');
    if (menu) e.stopPropagation();
}, true);



