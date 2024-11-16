import { AddEvents as EventSidebar, cargarSidebar } from '../slider/slider-functions.js'
import { loading } from '../components/loading.js'
import { AddEvents } from './inventario-funtions.js'

window.onload = function () {
    EventSidebar()
    AddEvents()
    cargarSidebar()
}

window.addEventListener('DOMContentLoaded', () => {
    loading()
})