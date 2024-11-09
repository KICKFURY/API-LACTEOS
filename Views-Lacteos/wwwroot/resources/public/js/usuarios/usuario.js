import { AddEventsFirst } from './usuarios-functios.js'
import { AddEvents as EventSidebar, cargarSidebar } from '../slider/slider-functions.js'

window.onload = function () {
    AddEventsFirst()
    EventSidebar()
    cargarSidebar()
    document.getElementById('buscarUsername').style.display = 'none'
    document.getElementById('lbaBuscador').style.display = 'none'
    document.querySelector('.form-section').style.marginLeft = '-100px'
}