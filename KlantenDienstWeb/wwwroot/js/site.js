// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

"use strict";

<script>
  // Voorkom dat de dropdown sluit bij klikken binnen de menu
    document.addEventListener('click', function (e) {
    const menu = e.target.closest('.dropdown-menu');
    if (menu) e.stopPropagation();
  });

    // Optioneel: als je op de toggle klikt, ook niet laten submitten/closingen
    document.addEventListener('click', function (e) {
    const toggle = e.target.closest('.cat-toggle');
    if (toggle) e.preventDefault();
  });
</script>

