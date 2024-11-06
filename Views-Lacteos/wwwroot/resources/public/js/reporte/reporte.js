import { AddEvents, RedirectURL } from './reporte-functions.js'
import { cargarSidebar, AddEvents as EventSlider } from "../slider/slider-functions.js"

window.onload = function () {
    AddEvents()
    EventSlider()
    cargarSidebar()
    RedirectURL()
}