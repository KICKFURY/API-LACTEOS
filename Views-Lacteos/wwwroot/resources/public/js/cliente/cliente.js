import { AddEvents, ObtenerClientes } from './functions-clientes.js'
import { cargarSidebar, AddEvents as EventSlider } from "../slider/slider-functions.js"
import { loading } from '../components/loading.js'

window.onload = function () {
    AddEvents()
    EventSlider()
    ObtenerClientes()
    cargarSidebar()
    document.getElementById('Busquedacedula').style.display ='none'
    document.getElementById('lbBuscador').style.display = 'none'
    document.querySelector('.buttons').style.marginTop = '40px'
}

window.addEventListener('DOMContentLoaded', loading)