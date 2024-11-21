import { AddEvents, cargarSidebar } from "../slider/slider-functions.js";
import { AddEventHome, RedirectURL } from "../home/home-functions.js";

window.onload = function () {
    AddEvents()
    AddEventHome()
    RedirectURL()
    cargarSidebar()
}