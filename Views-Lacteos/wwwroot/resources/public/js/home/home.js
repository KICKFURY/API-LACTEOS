import { AddEvents, cargarSidebar } from "../slider/slider-functions.js";
import { RedirectURL } from "../home/home-functions.js";
import Loader from '../components/loading.js'

const loader = new Loader()


window.onload = function () {
    AddEvents()
    RedirectURL()
    cargarSidebar()
}

window.addEventListener('DOMContentLoaded', () => {
    loader.hide()
})