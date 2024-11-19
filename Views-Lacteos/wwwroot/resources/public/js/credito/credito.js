import { AddEvents } from './credito-functions.js'
import { AddEvents as EventSidebar, cargarSidebar } from '../slider/slider-functions.js'

window.onload = function () {
    EventSidebar()
    cargarSidebar()
    AddEvents()
}