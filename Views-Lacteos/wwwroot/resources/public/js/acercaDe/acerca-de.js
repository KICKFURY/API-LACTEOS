import { AddEvents as EventSidebar, cargarSidebar} from '../slider/slider-functions.js'
import { AddEvents } from './acerca-de-functios.js'
import Loader from '../components/loading.js'

const loader = new Loader()

window.onload = function () {
    EventSidebar()
    AddEvents()
    cargarSidebar()
}

window.addEventListener('DOMContentLoaded', () => {
    loader.hide()
})