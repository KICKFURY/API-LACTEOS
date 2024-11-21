import { AddEvents as EventSidebar, cargarSidebar } from '../slider/slider-functions.js'
import { AddEvents } from './Mantenimiento-functions.js'
import { loading } from '../components/loading.js'

window.onload = function () {
    AddEvents()
    EventSidebar()
    cargarSidebar()
}

window.addEventListener('DOMContentLoaded', () => {
    loading()
})