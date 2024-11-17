import { AddEvents as EventSidebar, cargarSidebar } from '../slider/slider-functions.js'
import { loading } from '../components/loading.js'
import { AddEvents, GetProductos } from './inventario-funtions.js'

window.onload = function () {
    EventSidebar()
    AddEvents()
    cargarSidebar()
    document.getElementById('Busquedacedula').style.display = 'none'
    document.getElementById('lbBuscador').style.display = 'none'
}

window.addEventListener('DOMContentLoaded', () => {
    GetProductos()
    loading()
})