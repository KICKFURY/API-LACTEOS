import { AddEventsFirst, OcultarBuscadorUsername } from './usuarios-functios.js'
import { AddEvents as EventSidebar, cargarSidebar } from '../slider/slider-functions.js'

window.onload = function () {
    AddEventsFirst()
    EventSidebar()
    cargarSidebar()
    OcultarBuscadorUsername()
    document.getElementById('estado').disabled = true
}