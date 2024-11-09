import { GET_Clientes, GET_Cliente, POST_Cliente, PUT_Cliente, DELETE_Cliente } from "../endpoints.js";
import { GET, POST, PUT, DELETE } from '../generic-functions.js';

function AddEvents() {
    document.getElementById('btnCrear').addEventListener('click', CrearCliente);
    document.getElementById('btnEditar').addEventListener('click', EditarCliente);
    document.getElementById('btnEliminar').addEventListener('click', EliminarCliente);
    document.getElementById('btnCancelar').addEventListener('click', LimpiarControles); 

    document.getElementById('rdaEditar').addEventListener('change', () => {
        MostrarBuscadorRUC()
        document.getElementById('btnCrear').disabled = true
        document.getElementById('btnEditar').disabled = false
        document.getElementById('btnEliminar').disabled = true
    })
    document.getElementById('rdaEliminar').addEventListener('change', () => {
        MostrarBuscadorRUC()
        document.getElementById('btnCrear').disabled = true
        document.getElementById('btnEditar').disabled = true
        document.getElementById('btnEliminar').disabled = false
    })
    document.getElementById('rdaCrear').addEventListener('change', RDACREARINTERNO)
    document.getElementById('btnEditar').addEventListener('click', () => {
        EditarProveedor()
    })
    document.getElementById('Busquedacedula').addEventListener('keyup', () => {
        var cedula = document.getElementById('Busquedacedula').value
        if (cedula.length == 14) {
            ActualizarClientes()
        }
    })
    document.getElementById('ruc').addEventListener('keyup', () => {
        var cedula = document.getElementById('ruc').value
        if (cedula.length == 14) {
            ObtenerClientes()
        }
    })
}


function ObtenerClientes() {
    GET(GET_Clientes, "Error al cargar el listado de clientes", 1, (data) => {
        const clientesTabla = document.getElementById('tablaClientes').getElementsByTagName('tbody')[0];
        clientesTabla.innerHTML = ''; 
        data.response.forEach(cliente => {
            let row = clientesTabla.insertRow();
            row.innerHTML = `
                <td>${cliente.nombreCliente}</td>
                <td>${cliente.apellidoCliente}</td>
                <td>${cliente.ruc}</td>
                <td>${cliente.direccion}</td>
                <td>${cliente.telefono}</td>
            `;
        });
    });
}


function CrearCliente() {
    var nombre = document.getElementById('txtNombre').value;
    var apellido = document.getElementById('txtApellido').value;
    var ruc = document.getElementById('txtRUC').value;
    var telefono = document.getElementById('txtTelefono').value;
    var direccion = document.getElementById('txtDireccion').value;
    

    var url = `${POST_Cliente}${nombre}&${apellido}&${ruc}?direccion=${direccion}&telefono=${telefono}`;


        POST(url, "Cliente Creado Exitosamente", "Error al crear el cliente", () => {
            ObtenerClientes(); 
            LimpiarControles(); 
        });
    }


function EditarCliente() {
    var nombre = document.getElementById('txtNombre').value;
    var apellido = document.getElementById('txtApellido').value;
    var ruc = document.getElementById('txtRUC').value;
    var telefono = document.getElementById('txtTelefono').value;
    var direccion = document.getElementById('txtDireccion').value;
    
    var url = `${PUT_Cliente}${nombre}&${apellido}&${ruc}?direccion=${direccion}&telefono=${telefono}`;

    PUT(url, "Cliente Editado Exitosamente", "Error al editar el cliente", () => {
        ObtenerClientes();  
        LimpiarControles();
    });
}



function EliminarCliente() {
    var ruc = document.getElementById('txtRUC').value;
    var url = DELETE_Cliente + ruc;

    DELETE(url, "Cliente eliminado correctamente", "Error al eliminar el cliente", () => {
        ObtenerClientes();  
        LimpiarControles();
    });
}


function LimpiarControles() {
    document.getElementById('txtNombre').value = '';
    document.getElementById('txtApellido').value = '';
    document.getElementById('txtRUC').value = '';
    document.getElementById('txtTelefono').value = '';
    document.getElementById('txtDireccion').value = '';
}

function ActualizarClientes(){
    var ruc = document.getElementById('Busquedacedula').value
    var nombre = document.getElementById('txtNombre')
    var apellido = document.getElementById('txtApellido')
    var telefono = document.getElementById('txtTelefono')
    var cedula = document.getElementById('txtRUC')
    var direccion = document.getElementById('txtDireccion')
    var url = GET_Cliente+ruc

    GET(url,"Error al Obtener el cliente", 1, (data) =>{
        console.log(data)
        nombre.value = data.response.nombreCliente
        apellido.value = data.response.apellidoCliente
        telefono.value = data.response.telefono
        cedula.value = data.response.ruc
        direccion.value = data.response.direccion
    })
}

function RDACREARINTERNO() {
    OcultarBuscadorRUC()
    document.getElementById('btnCrear').disabled = false
    document.getElementById('btnEditar').disabled = true
    document.getElementById('btnEliminar').disabled = true
}

function MostrarBuscadorRUC(){
    LimpiarControles()
    document.getElementById('Busquedacedula').style.display = ''
    document.getElementById('lbBuscador').style.display = ''
    document.querySelector('.buttons').style.marginTop = '-40px'
}

function OcultarBuscadorRUC(){
    LimpiarControles()
    document.getElementById('Busquedacedula').style.display ='none'
    document.getElementById('lbBuscador').style.display = 'none'
    document.querySelector('.buttons').style.marginTop = '40px'
}


export { AddEvents, ObtenerClientes }