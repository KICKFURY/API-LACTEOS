import { GET_Usuarios, GET_Login, POST_Usuario } from "../endpoints.js"
import { GET, POST } from "../generic-functions.js"
import { encriptar } from "../security.js"

function AddEvents() {
    document.getElementById('buscarUsername').addEventListener('keyup', () => {
        var username = document.getElementById('buscarUsername').value
        if (username.length >= 5) {
            CargarUsuario()
        }
    })

    document.getElementById('btnCargarTabla').addEventListener('click', () => {
        ObtenerUsuarios()
        document.getElementById('buscarUsername').value = ''
    })

    var btn = document.getElementById('btnGenerarReporte')
    var role = localStorage.getItem('UsuarioRole');

    if (role == 'Admin') {
        btn.style.display = ''
    } else if (role == 'Vendedor') {
        btn.style.display = 'none'
    } else if (role == 'Encargado de Inventario') {
        btn.style.display = 'none'
    }

    btn.addEventListener('click', () => {
        window.reporte.showModal()
    })
    
    document.getElementById('reporte1').addEventListener('click', ()=>{
        window.reporte.close()
    })
}


function AddEventsFirst() {
    document.getElementById('rdaCrear').addEventListener('change', RDACREARINTERNO)
    document.getElementById('rdaEditar').addEventListener('change', () => {
        MostrarBuscadorUsername()
        ActivateControl(false)
        document.getElementById('btnCrear').disabled = true
        document.getElementById('btnEditar').disabled = false
        document.getElementById('btnEliminar').disabled = true
        document.getElementById('estado').disabled = false
    })
    document.getElementById('rdaEliminar').addEventListener('change', () => {
        MostrarBuscadorUsername()
        ActivateControl(true)
        document.getElementById('btnCrear').disabled = true
        document.getElementById('btnEditar').disabled = true
        document.getElementById('btnEliminar').disabled = false
        document.getElementById('estado').disabled = false
    })

    document.getElementById('buscarUsername').addEventListener('keyup', () => {
        var username = document.getElementById('buscarUsername').value
        if (username.length == 5) {
            CargarUsuarioCampos()
        }
    })

    document.getElementById('btnCancelar').addEventListener('click', LimpiarControles)
    document.getElementById('btnCrear').addEventListener('click', CrearUsuario)
}

function CargarUsuarioCampos() {
    var username = document.getElementById('nombreUsuario').value
    var correo = document.getElementById('txtCorreo').value
    var role = document.getElementById('rolGeneral').value
    var psw1 = document.getElementById('txtPSW1').value
    var psw2 = document.getElementById('txtPSW2').value
    var url = GET_Login+username

    GET(url,  "Error al obtener el usuario", 1, (data) => { 
        username = data.response.nombreUsuario
        correo = data.response.correo
        role = data.response.rolGeneral == 2 ? "Vendedor" : "Encargado de Inventario"
        
    })
}

function CargarUsuario() {
    var  username = document.getElementById('buscarUsername').value
    var url = GET_Login+username

    GET(url,  "Error al obtener el usuario", 1, (data) => { 
        const proveedoresTabla = document.getElementById('tablaUsuarios').getElementsByTagName('tbody')[0]
        proveedoresTabla.innerHTML = ''
        let row = proveedoresTabla.insertRow();
        row.innerHTML = `
                <td>${data.response.nombreUsuario}</td>
                <td>${data.response.contra}</td>
                <td>${data.response.correo}</td>
                <td>${data.response.idEstado == 1 ? 'Activo' : 'Inactivo'}</td>
            `
    })
}

function ObtenerUsuarios() {
    var url = GET_Usuarios

    GET(url, "Error al obtener la lista de usuarios", 1, (data) => {
        const usuariosTabla = document.getElementById('tablaUsuarios').getElementsByTagName('tbody')[0]
        usuariosTabla.innerHTML = ''
        data.response.forEach(usuario => {
            let row = usuariosTabla.insertRow();
            row.innerHTML = `
                <td>${usuario.nombreUsuario}</td>
                <td>${usuario.contra}</td>
                <td>${usuario.correo}</td>
                <td>${usuario.idEstado == 1 ? 'Activo' : 'Inactivo'}</td>
            `
        })
    })
}

function CrearUsuario() {
    var username = document.getElementById('nombreUsuario').value
    var correo = document.getElementById('txtCorreo').value
    var role = document.getElementById('rolGeneral').value
    var psw1 = document.getElementById('txtPSW1').value
    var psw2 = document.getElementById('txtPSW2').value

    if ( psw1!= psw2) {
        Swal.fire({
            title: 'Error',
            text: 'Las contraseñas deben de conincidir',
            icon: 'error',
            confirmButtonText: 'Aceptar',
            confirmButtonColor: '#7a2a1e',
        });
        return
    }

    if (username == '' || correo == '' || psw1 == '' || psw2 == '' || role == '') {
        Swal.fire({
            title: 'Error',
            text: 'Todos los campos son obligatorios',
            icon: 'error',
            confirmButtonText: 'Aceptar',
            // showCancelButton: true,
            confirmButtonColor: '#7a2a1e',
            // cancelButtonColor: '#d33',
            // cancelButtonText: 'Cancelar'
        });
        return
    }

    var hash = encriptar(psw1)
    const url = `${POST_Usuario}${correo}&${username}&${hash}&${role == 'Vendedor' ? 2 : 3}`

    POST(url, "Usuario Creado", "Error al crear el usuario", () => {
        LimpiarControles()
    })
}

function ActivateControl(value) {
    document.getElementById('buscarUsername').disabled = value
    document.getElementById('nombreUsuario').disabled = value
    document.getElementById('txtCorreo').disabled = value
    document.getElementById('txtPSW1').disabled = value
    document.getElementById('txtPSW2').disabled = value
}

function RDACREARINTERNO() {
    OcultarBuscadorUsername()
    document.getElementById('btnCrear').disabled = false
    document.getElementById('btnEditar').disabled = true
    document.getElementById('btnEliminar').disabled = true
    document.getElementById('estado').disabled = true
}

function OcultarBuscadorUsername() {
    LimpiarControles()
    document.getElementById('buscarUsername').style.display = 'none'
    document.getElementById('lbaBuscador').style.display = 'none'
    document.querySelector('.form-section').style.marginLeft = '-100px'
}

function MostrarBuscadorUsername() {
    LimpiarControles()
    document.getElementById('buscarUsername').style.display = ''
    document.getElementById('lbaBuscador').style.display = ''
    document.querySelector('.form-section').style.marginLeft = '-414px'
}

function LimpiarControles() {
    document.getElementById('buscarUsername').value = ''
    document.getElementById('nombreUsuario').value = ''
    document.getElementById('txtCorreo').value = ''
    document.getElementById('txtPSW1').value = ''
    document.getElementById('txtPSW2').value = ''
}

export { AddEventsFirst, AddEvents, ObtenerUsuarios }