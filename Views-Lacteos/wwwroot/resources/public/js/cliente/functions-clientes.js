import { GET_Clientes, GET_Cliente, POST_Cliente, PUT_Cliente, DELETE_Cliente, Cliente_vendedor, clientes_admin } from "../endpoints.js";
import { GET, POST, PUT, DELETE } from '../generic-functions.js';
import { Alerta } from '../components/alert.js'
import { copyToClipboard } from "../components/CopyToClipboard.js";

function AddEvents() {
    document.getElementById('btnEditar').disabled = true
    document.getElementById('btnEliminar').disabled = true
    document.getElementById('btnCrear').addEventListener('click', CrearCliente);
    document.getElementById('btnEditar').addEventListener('click', EditarCliente);
    document.getElementById('btnEliminar').addEventListener('click', EliminarCliente);
    document.getElementById('btnCancelar').addEventListener('click', LimpiarControles); 

    document.getElementById('rdaEditar').addEventListener('change', () => {
        MostrarBuscadorRUC()
        document.getElementById('btnCrear').disabled = true
        document.getElementById('btnEditar').disabled = false
        document.getElementById('btnEliminar').disabled = true
        document.getElementById('Busquedacedula').value = '';
        LimpiarControles()
    })
    document.getElementById('rdaEliminar').addEventListener('change', () => {
        MostrarBuscadorRUC()
        document.getElementById('btnCrear').disabled = true
        document.getElementById('btnEditar').disabled = true
        document.getElementById('btnEliminar').disabled = false
        document.getElementById('Busquedacedula').value = '';
        LimpiarControles()
    })
    document.getElementById('rdaCrear').addEventListener('change', () => {
        RDACREARINTERNO()
        document.getElementById('Busquedacedula').value = '';
        LimpiarControles()
    })
    document.getElementById('btnEditar').addEventListener('click', () => {
        EditarProveedor()
    })
    document.getElementById('Busquedacedula').addEventListener('keyup', () => {
        var cedula = document.getElementById('Busquedacedula').value
        if (cedula.length == 14) {
            ActualizarClientes()
        } else {
            LimpiarControles()
        }
    })
    document.getElementById('ruc').addEventListener('keyup', () => {
        var cedula = document.getElementById('ruc').value
        if (cedula.length == 14) {
            ObtenerClientes()
        }
    })

    var btn = document.getElementById('btnGenerarReporte')

    btn.addEventListener('click', () => {
        window.reporte.showModal()
    })
    
    document.getElementById('reporte1').addEventListener('click', ()=>{
        window.reporte.close()
    })

    var role = localStorage.getItem('UsuarioRole');

    if (role == 'Admin') {
        btn.style.display = ''
    } else if (role == 'Vendedor') {
        btn.style.display = 'none'
    } else if (role == 'Encargado de Inventario') {
        btn.style.display = 'none'
    }

    document.getElementById('btnManual').addEventListener('click', () => {
        window.manual.showModal()
    })

    document.getElementById('manual2').addEventListener('click', () => {
        window.manual.close()
    })
}


function ObtenerClientes() {
    GET(GET_Clientes, "Error al cargar el listado de clientes", 1, (data) => {
        const clientesTabla = document.getElementById('tablaClientes').getElementsByTagName('tbody')[0];
        clientesTabla.innerHTML = ''; 
        data.response.forEach((cliente, index) => {
            let row = clientesTabla.insertRow();
            row.innerHTML = `
                <td>${cliente.nombreCliente}</td>
                <td>${cliente.apellidoCliente}</td>
                <td id="copyRuc${index}">${cliente.ruc}</td>
                <td>${cliente.direccion}</td>
                <td>${cliente.telefono}</td>
            `;

            document.getElementById(`copyRuc${index}`).addEventListener('click', (e) => {
                const text = e.target.innerText;
                copyToClipboard(text)
            })
        });
    }, () => {});
}


function CrearCliente() {
    var nombre = document.getElementById('txtNombre').value;
    var apellido = document.getElementById('txtApellido').value;
    var ruc = document.getElementById('txtRUC').value;
    var telefono = document.getElementById('txtTelefono').value;
    var direccion = document.getElementById('txtDireccion').value;

    if (nombre == '' || apellido == '' || ruc == '') {
        Alerta("Error", "Todos los campos son obligatorios", "error");
        return;
    }

    var url = `${POST_Cliente}${nombre}&${apellido}&${ruc}?direccion=${direccion}&telefono=${telefono}`;

    POST(url, "Cliente Creado Exitosamente", "Error al crear el cliente", () => {
        ObtenerClientes(); 
        Alerta("Confirmado", "Cliente creado correctamente", "success");
        LimpiarControles();
        document.getElementById('Busquedacedula').value = '';
    });
}


function EditarCliente() {
    var nombre = document.getElementById('txtNombre').value;
    var apellido = document.getElementById('txtApellido').value;
    var ruc = document.getElementById('txtRUC').value;
    var telefono = document.getElementById('txtTelefono').value;
    var direccion = document.getElementById('txtDireccion').value;

    if (nombre == '' || apellido == '' || ruc == '') {
        Alerta("Error", "Todos los campos son obligatorios", "error");
        return;
    }
    
    var url = `${PUT_Cliente}${nombre}&${apellido}&${ruc}?direccion=${direccion}&telefono=${telefono}`;

    PUT(url, "Cliente Editado Exitosamente", "Error al editar el cliente", () => {
        ObtenerClientes();
        Alerta("Confirmado", "Cliente editado correctamente", "success");
        LimpiarControles();
        document.getElementById('Busquedacedula').value = '';
    });
}



function EliminarCliente() {
    var ruc = document.getElementById('txtRUC').value;
    var url = DELETE_Cliente + ruc;

    if (ruc == '') {
        Alerta("Error", "La cedula del cliente es obligatorio", "error");
        return;
    }

    DELETE(url, "Cliente eliminado correctamente", "Error al eliminar el cliente", () => {
        ObtenerClientes();
        Alerta("Confirmado", "Cliente eliminado correctamente", "success");
        LimpiarControles();
        document.getElementById('Busquedacedula').value = '';
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

    GET(url,"Error al Obtener el cliente", 1, (data) => {
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