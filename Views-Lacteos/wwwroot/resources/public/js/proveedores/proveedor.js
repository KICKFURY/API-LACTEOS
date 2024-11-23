import { ObtenerProveedores, AddEvents } from "./funciones-proveedor.js"

window.onload = function () {
    AddEvents()
    ObtenerProveedores()
    document.getElementById('Busquedacedula').style.display = 'none'
    document.getElementById('lbaBuscador').style.display = 'none'
}