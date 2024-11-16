import { AddEvents as EventSidebar, cargarSidebar } from '../slider/slider-functions.js'
import { AddEvents as EventsComrpas } from './compras-functions.js'
import { loading } from '../components/loading.js'

window.onload = function () {
    EventSidebar()
    cargarSidebar()
    EventsComrpas()
}

window.addEventListener('DOMContentLoaded', () => {
    loading()
})