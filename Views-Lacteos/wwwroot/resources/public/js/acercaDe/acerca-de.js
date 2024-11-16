import { AddEvents, cargarSidebar} from '../slider/slider-functions.js'
import { loading } from '../components/loading.js'

window.onload = function () {
    AddEvents()
    cargarSidebar()
}

window.addEventListener('DOMContentLoaded', () => {
    loading()
})