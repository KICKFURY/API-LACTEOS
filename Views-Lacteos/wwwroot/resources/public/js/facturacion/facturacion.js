import { AddEvents } from './facturacion-function.js'
import { cargarSidebar, AddEvents as EventsSiderbar } from "../slider/slider-functions.js";

window.onload = function () {
    AddEvents()
    EventsSiderbar()
    cargarSidebar()
}