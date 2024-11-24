import { AddEvents, ObtenerClientes } from './functions-clientes.js'

window.onload = function () {
    AddEvents()
    ObtenerClientes()
    document.getElementById('Busquedacedula').style.display ='none'
    document.getElementById('lbBuscador').style.display = 'none'
    document.querySelector('.buttons').style.marginTop = '40px'
}