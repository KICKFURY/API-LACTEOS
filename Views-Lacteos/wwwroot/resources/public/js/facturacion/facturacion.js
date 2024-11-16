import { AddEvents } from './facturacion-function.js'
import { cargarSidebar } from "../slider/slider-functions.js";
import { loading } from '../components/loading.js'


window.onload = async function () {
    AddEvents()
    cargarSidebar()
}


window.addEventListener('DOMContentLoaded', () => {
    loading()
})