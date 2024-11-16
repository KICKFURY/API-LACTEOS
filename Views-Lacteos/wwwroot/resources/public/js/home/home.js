import { AddEvents, cargarSidebar } from "../slider/slider-functions.js";
import { RedirectURL } from "../home/home-functions.js";
import { loading } from "../components/loading.js";


window.onload = function () {
    AddEvents()
    RedirectURL()
    cargarSidebar()
}

window.addEventListener('DOMContentLoaded', () => {
    loading()
})