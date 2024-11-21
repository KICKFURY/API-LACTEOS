import { AddEvents as EventSidebar, cargarSidebar } from '../slider/slider-functions.js'
import { AddEvents as EventsComrpas } from './compras-functions.js'

window.onload = function () {
    EventSidebar()
    cargarSidebar()
    EventsComrpas()
}