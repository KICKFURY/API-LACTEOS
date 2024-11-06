import { GET_Clientes, POST_Cliente, PUT_Cliente, DELETE_Cliente } from "../endpoints.js";
import { GET, POST, PUT, DELETE } from '../generic-functions.js'

function AddEvents() {
    document.getElementById('btnCrear').addEventListener('click', CrearCliente)
    document.getElementById('btnEditar').addEventListener('click', EditarCliente)
    document.getElementById('btnEliminar').addEventListener('click', EliminarCliente)
    document.getElementById('btnCancelar').addEventListener('click', LimpiarControles)
}

function ObtenerClientes() {
    GET(GET_Clientes, "Error al cargar es listado de clientes", 1, (data) => {
        const proveedoresTabla = document.getElementById('tablaClientes').getElementsByTagName('tbody')[0]
        proveedoresTabla.innerHTML = ''
        data.response.forEach(cliente => {
            let row = proveedoresTabla.insertRow();
            row.innerHTML = `
            <td>${cliente.nombreCliente}</td>
            <td>${cliente.apellidoCliente}</td>
            <td>${cliente.ruc}</td>
            <td>${cliente.direccion}</td>
            <td>${cliente.telefono}</td>
        `
        })
    })
}

function CrearCliente() {
    var nombre = document.getElementById('txtNombre').value
    var apellido = document.getElementById('txtApellido').value
    var ruc = document.getElementById('txtRUC').value
    var telefono = document.getElementById('txtTelefono').value
    var direccion = document.getElementById('txtDireccion').value
    var url = `${POST_Cliente}${nombre}&&${apellido}&&${ruc}?direccion=${direccion}&telefono=${telefono}`

    POST(url, "Cliente Creado Exitosamente", "Error al crear el cliente", () => {
        ObtenerClientes()
        LimpiarControles()
    })
}

function EditarCliente() {
    var nombre = document.getElementById('txtNombre').value
    var apellido = document.getElementById('txtApellido').value
    var ruc = document.getElementById('txtRUC').value
    var telefono = document.getElementById('txtTelefono').value
    var direccion = document.getElementById('txtDireccion').value
    var url = `${PUT_Cliente}${nombre}&${apellido}&${ruc}?direccion=${direccion}&telefono=${telefono}`

    PUT(url, "Cliente Editado Exitosamente", "Error al editar el cliente", () => {
        ObtenerClientes()
        LimpiarControles()
    })
}

function EliminarCliente() {
    var ruc = document.getElementById('txtRUC').value
    var url = DELETE_Cliente+ruc

    DELETE(url, "Cliente eliminado correctamente", "Error al eliminar el cliente", () => {
        ObtenerClientes()
        LimpiarControles()
    })
}

function LimpiarControles() {
    document.getElementById('txtNombre').value = ""
    document.getElementById('txtApellido').value = ""
    document.getElementById('txtRUC').value = ""
    document.getElementById('txtTelefono').value = ""
    document.getElementById('txtDireccion').value = ""
}

export { AddEvents, ObtenerClientes }