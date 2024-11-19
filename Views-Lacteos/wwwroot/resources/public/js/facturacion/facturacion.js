import { AddEvents } from './facturacion-function.js'
import { cargarSidebar, AddEvents as EventSidebar } from "../slider/slider-functions.js";
import { loading } from '../components/loading.js'


window.onload = async function () {
    AddEvents()
    cargarSidebar()
    EventSidebar()
}


window.addEventListener('DOMContentLoaded', () => {
    loading()
})