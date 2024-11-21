import { AddEvents, cargarSidebar} from '../slider/slider-functions.js'
import Loader from '../components/loading.js'

const loader = new Loader()

window.onload = function () {
    AddEvents()
    cargarSidebar()
}

window.addEventListener('DOMContentLoaded', () => {
    loader.hide()
})