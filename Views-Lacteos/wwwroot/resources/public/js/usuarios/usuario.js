import { AddEventsFirst, OcultarBuscadorUsername } from './usuarios-functios.js'

window.onload = function () {
    AddEventsFirst()
    OcultarBuscadorUsername()
    document.getElementById('estado').disabled = true
}