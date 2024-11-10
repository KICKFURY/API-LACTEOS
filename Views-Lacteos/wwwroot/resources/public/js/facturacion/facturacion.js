import { AddEvents } from './facturacion-function.js'
import { cargarSidebar, AddEvents as EventsSiderbar } from "../slider/slider-functions.js";
import { loading } from '../components/loading.js'

window.onload = function () {
    AddEvents()
    EventsSiderbar()
    cargarSidebar()
}


window.addEventListener('DOMContentLoaded', () => {
    loading()
})