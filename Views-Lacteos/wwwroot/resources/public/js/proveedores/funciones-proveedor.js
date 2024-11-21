import { GET, POST, PUT, DELETE } from '../generic-functions.js'
import { POST_Proveedor, PUT_Proveedor, DELETE_Proveedor, GET_Proveedores, GET_Proveedor, proveedor_admin, proveedor_encargado_inventario } from '../endpoints.js'
import { Alerta } from '../components/alert.js'
import { copyToClipboard } from '../components/CopyToClipboard.js'
import Loader from '../components/loading.js'

const loader = new Loader();

function AddEvents() {
    document.getElementById("estado").disabled = true
    document.getElementById('btnCrear').addEventListener('click', () => {
        CrearProveedor()
    })
    document.getElementById('btnEliminar').addEventListener('click', EliminarProveedor)
    document.getElementById('rdaCrear').addEventListener('change', () => {
        RDACREARINTERNO()
        document.getElementById("cedula").disabled = false
        document.getElementById("estado").disabled = true
    })
    document.getElementById('btnCargar').addEventListener('click', () => {
        ObtenerProveedores()
        document.getElementById('ruc').value = ''
    })
    document.getElementById('rdaEditar').addEventListener('change', () => {
        MostrarBuscadorRUC()
        document.getElementById('btnCrear').disabled = true
        document.getElementById('btnEditar').disabled = false
        document.getElementById('btnEliminar').disabled = true
        DesactivarControles(false)
        document.getElementById("cedula").disabled = true
        document.getElementById("estado").disabled = false
    })
    document.getElementById('rdaEliminar').addEventListener('change', () => {
        MostrarBuscadorRUC()
        document.getElementById('btnCrear').disabled = true
        document.getElementById('btnEditar').disabled = true
        document.getElementById('btnEliminar').disabled = false
        DesactivarControles(true)
    })
    document.getElementById('btnEditar').addEventListener('click', () => {
        EditarProveedor()
    })
    document.getElementById('Busquedacedula').addEventListener('keyup', () => {
        var cedula = document.getElementById('Busquedacedula').value
        if (cedula.length == 14) {
            CargarProveedor()
        }
    })
    document.getElementById('ruc').addEventListener('keyup', () => {
        var cedula = document.getElementById('ruc').value
        if (cedula.length == 14) {
            CargarProveedorEnTabla()
        }
    })

    var btn = document.getElementById('btnGenerarReporte')
    var role = localStorage.getItem('UsuarioRole');

    if (role == 'Admin') {
        document.getElementById('manual1').src = proveedor_admin
        btn.style.display = ''
    } else if (role == 'Vendedor') {
        btn.style.display = 'none'
    } else if (role == 'Encargado de Inventario') {
        document.getElementById('manual1').src = proveedor_encargado_inventario
        btn.style.display = 'none'
    }

    btn.addEventListener('click', () => {
        window.reporte.showModal()
    })
    
    document.getElementById('reporte1').addEventListener('click', ()=>{
        window.reporte.close()
    })

    document.getElementById('btnManual').addEventListener('click', () => {
        
        window.manual.showModal()
    })

    document.getElementById('manual2').addEventListener('click', () => {
        window.manual.close()
    })
}

function CrearProveedor() {
    var nombre = document.getElementById('nombre').value
    var telefono = document.getElementById('telefono').value
    var cedula = document.getElementById('cedula').value

    if (nombre == '' || telefono == '' || cedula == '') {
        Alerta("Error", "Rellene los campos obligatorios", "error")
    }

    var url = `${POST_Proveedor}${nombre}&${telefono}&${cedula}`

    POST(url, "Proveedor guardado", "Error al guardar el proveedor", () => {
        ObtenerProveedores()
        LimpiarControles()
    })
}

function EditarProveedor() {
    var nombre = document.getElementById('nombre').value
    var telefono = document.getElementById('telefono').value
    var ruc = document.getElementById('cedula').value
    var id = document.getElementById('estado').value

    if (nombre == '' || telefono == '' || cedula == '') {
        Alerta("Error", "Rellene los campos obligatorios", "error")
    }

    var url = `${PUT_Proveedor}${nombre}&${ruc}&${telefono}&${id == 'Activo' ? 1 : 2}`

    PUT(url, "Proveedor editado", "Error al editar el proveedor", () => {
        ObtenerProveedores()
        LimpiarControles()
    })
}

function EliminarProveedor() {
    var ruc = document.getElementById('Busquedacedula').value
    var url = DELETE_Proveedor+ruc

    if (ruc == '') {
        Alerta("Error", "La cedula del proveedor es obligatoria", "error")
        return;
    }

    DELETE(url, "Proveedor Eliminado", "Error al eliminar el proveedor.", () => {
        ObtenerProveedores()
        LimpiarControles()
    })
}

function ObtenerProveedores() {
    var url = GET_Proveedores

    GET(url, "Error al obtener la lista de proveedores", 1, (data) => {
        const proveedoresTabla = document.getElementById('idProveedoresTabla').getElementsByTagName('tbody')[0]
        proveedoresTabla.innerHTML = ''
        data.response.forEach((proveedor, index) => {
            let row = proveedoresTabla.insertRow();
            row.innerHTML = `
                <td>${proveedor.nombreProveedor}</td>
                <td>${proveedor.telefonoProveedor}</td>
                <td id="copyRuc${index}">${proveedor.rucProveedor}</td>
                <td>${proveedor.idEstado == 1 ? 'Activo' : 'Inactivo'}</td>
            `

            document.getElementById(`copyRuc${index}`).addEventListener('click', (e) => {
                const text = e.target.innerText;
                copyToClipboard(text)
            })
        })
        loader.hide()
    })
}

function LimpiarControles() {
    document.getElementById('nombre').value = ''
    document.getElementById('telefono').value = ''
    document.getElementById('cedula').value = ''
    document.getElementById('Busquedacedula').value = ''
}

function CargarProveedor() {
    var ruc = document.getElementById('Busquedacedula').value
    var nombre = document.getElementById('nombre')
    var telefono = document.getElementById('telefono')
    var cedula = document.getElementById('cedula')
    var url = GET_Proveedor+ruc

    GET(url, "Error al obtener el proveedor", 1, (data) => {
        nombre.value = data.response.nombreProveedor
        telefono.value = data.response.telefonoProveedor
        cedula.value = data.response.rucProveedor
    })
}

function CargarProveedorEnTabla() {
    var  ruc = document.getElementById('ruc').value
    var url = GET_Proveedor+ruc

    GET(url,  "Error al obtener el proveedor", 1, (data) => { 
        const proveedoresTabla = document.getElementById('idProveedoresTabla').getElementsByTagName('tbody')[0]
        proveedoresTabla.innerHTML = ''
        let row = proveedoresTabla.insertRow();
        row.innerHTML = `
            <td>${data.response.nombreProveedor}</td>
            <td>${data.response.telefonoProveedor}</td>
            <td>${data.response.rucProveedor}</td>
            <td>${data.response.idEstado == 1 ? 'Activo' : 'Inactivo'}</td>
        `
    }, () => {})
}

function RDACREARINTERNO() {
    OcultarBuscadorRUC()
    document.getElementById('btnCrear').disabled = false
    document.getElementById('btnEditar').disabled = true
    document.getElementById('btnEliminar').disabled = true
}

function MostrarBuscadorRUC() {
    LimpiarControles()
    document.getElementById('Busquedacedula').style.display = ''
    document.getElementById('lbaBuscador').style.display = ''
}

function OcultarBuscadorRUC() {
    LimpiarControles()
    document.getElementById('Busquedacedula').style.display = 'none'
    document.getElementById('lbaBuscador').style.display = 'none'
}

function DesactivarControles(value) {
    document.getElementById("nombre").disabled = value
    document.getElementById("cedula").disabled = value
    document.getElementById("telefono").disabled = value
    document.getElementById("estado").disabled = value
}

export { ObtenerProveedores, AddEvents }