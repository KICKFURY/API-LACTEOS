import { AddEvents, ObtenerClientes } from './functions-clientes.js'
import { cargarSidebar, AddEvents as EventSlider } from "../slider/slider-functions.js"

window.onload = function () {
    AddEvents()
    EventSlider()
    ObtenerClientes()
    cargarSidebar()
}