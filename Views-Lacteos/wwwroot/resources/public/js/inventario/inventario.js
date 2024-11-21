import { AddEvents as EventSidebar, cargarSidebar } from '../slider/slider-functions.js'
import { AddEvents } from './inventario-funtions.js'

window.onload = function () {
    EventSidebar()
    AddEvents()
    cargarSidebar()
    document.getElementById('Busquedacedula').style.display = 'none'
    document.getElementById('lbBuscador').style.display = 'none'
}