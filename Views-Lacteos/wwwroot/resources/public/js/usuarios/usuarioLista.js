import { AddEvents, ObtenerUsuarios } from './usuarios-functios.js'
import { AddEvents as EventSidebar, cargarSidebar } from '../slider/slider-functions.js'

window.onload = function () {
    AddEvents()
    ObtenerUsuarios()
    EventSidebar()
    
}

window.addEventListener('DOMContentLoaded', () => {
    cargarSidebar()
})