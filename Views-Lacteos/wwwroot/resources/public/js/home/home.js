import { cargarSidebar, AddEvents } from "../slider/slider-functions.js";
import { RedirectURL } from "../home/home-functions.js";

window.onload = function () {
    AddEvents()
    RedirectURL()
    cargarSidebar()
}

// window.addEventListener('DOMContentLoaded', () => {
//     cargarSidebar()
// })