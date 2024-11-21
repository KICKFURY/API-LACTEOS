import { AddEvents } from './facturacion-function.js'
import { cargarSidebar, AddEvents as EventSidebar } from "../slider/slider-functions.js";

window.onload = async function () {
    AddEvents()
    cargarSidebar()
    EventSidebar()
}