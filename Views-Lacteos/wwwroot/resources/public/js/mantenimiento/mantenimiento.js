import { AddEvents as EventSidebar, cargarSidebar } from '../slider/slider-functions.js'
import { loading } from '../components/loading.js'

window.onload = function () {
    EventSidebar()
    cargarSidebar()
}

window.addEventListener('DOMContentLoaded', () => {
    loading()
})