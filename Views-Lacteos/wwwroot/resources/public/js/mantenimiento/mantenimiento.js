import { AddEvents as EventSidebar, cargarSidebar } from '../slider/slider-functions.js'
import { AddEvents } from './Mantenimiento-functions.js'

window.onload = function () {
    AddEvents()
    EventSidebar()
    cargarSidebar()
}