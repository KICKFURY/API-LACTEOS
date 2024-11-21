import { ObtenerProveedores, AddEvents } from "./funciones-proveedor.js"
import { cargarSidebar, AddEvents as EventSlider } from "../slider/slider-functions.js"

window.onload = function () {
    AddEvents()
    EventSlider()
    ObtenerProveedores()
    cargarSidebar()
    document.getElementById('Busquedacedula').style.display = 'none'
    document.getElementById('lbaBuscador').style.display = 'none'
}