import { GET_Usuarios, GET_Login, POST_Usuario, PUT_Usuario, DELETE_Usuario, usuarios_admin, usuarioLista_admin } from "../endpoints.js"
import { GET, POST, PUT, DELETE } from "../generic-functions.js"
import { encriptar } from "../security.js"
import Loader from '../components/loading.js'
import { Alerta } from "../components/alert.js"

const loader = new Loader()

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

    document.getElementById('btnManual').addEventListener('click', () => {
        document.getElementById('manual1').src = usuarioLista_admin
        window.manual.showModal()
    })

    document.getElementById('manual2').addEventListener('click', () => {
        window.manual.close()
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
        document.getElementById('estado').disabled = true
    })

    document.getElementById('buscarUsername').addEventListener('keyup', () => {
        var username = document.getElementById('buscarUsername').value

        if (username.length == '') {
            LimpiarControles()
            return
        }

        if (username.length >= 5) {
            CargarUsuarioCampos()
        } else {
            LimpiarControles()
        }
    })

    document.getElementById('btnEditar').addEventListener('click', () => {
        let username = document.getElementById('buscarUsername').value

        if (username <= 4) {
            Swal.fire({
                title: 'Error',
                text: 'Ingrese un nombre de usuario',
                icon: 'error',
                confirmButtonText: 'Aceptar',
                confirmButtonColor: '#7a2a1e',
            });
            LimpiarControles()
            return
        }

        EditarUsuario()
    })

    document.getElementById('btnEliminar').addEventListener('click', () => {
        window.dialogPSW.showModal()
    })

    document.getElementById('dialogPSW1').addEventListener('click', () => {
        window.dialogPSW.close()
    })
    document.getElementById('btnCancelar').addEventListener('click', LimpiarControles)
    document.getElementById('btnCrear').addEventListener('click', CrearUsuario)

    document.getElementById('btnConfirmar').addEventListener('click', () => {
        const pswGlobal = localStorage.getItem('UserPSW')
        const psw = encriptar(document.getElementById('txtPSWAdmin').value)
        var rdaEliminar = document.getElementById('rdaEliminar')
        var rdaEditar = document.getElementById('rdaEditar')

        console.log(pswGlobal == psw)
        console.log(psw)
        console.log(pswGlobal)
        if (pswGlobal == psw) {
            if (rdaEliminar.checked) {
                EliminarUsuario()
            } else if (rdaEditar.checked) {
                EditarUsuario()
            }
        } else {
            window.dialogPSW.close()
            Swal.fire({
                title: 'Error',
                text: 'La contraseña es incorrecta',
                icon: 'error',
                confirmButtonText: 'Aceptar',
                confirmButtonColor: '#7a2a1e',
            });
            LimpiarControles()
            document.getElementById('buscarUsername').value = ''
        }
    })

    document.getElementById('btnManual').addEventListener('click', () => {
        document.getElementById('manual1').src = usuarios_admin
        window.manual.showModal()
    })

    document.getElementById('manual2').addEventListener('click', () => {
        window.manual.close()
    })

    loader.hide()
}

function CargarUsuarioCampos() {
    var username = document.getElementById('buscarUsername').value
    var fecha = document.getElementById('fechaRegistro')
    var user = document.getElementById('nombreUsuario')
    var correo = document.getElementById('txtCorreo')
    var role = document.getElementById('rolGeneral')
    var estado = document.getElementById('estado')
    var url = GET_Login+username

    GET(url,  "Error al obtener el usuario", 1, (data) => {
        let date = data.response.fechaCreacion.toString().split('T')[0]
        fecha.value = date
        user.value = data.response.nombreUsuario
        correo.value = data.response.correo
        role.value = data.response.idRole == 1 ? "Admin" : data.response.idRole == 2 ? "Vendedor" : "Encargado de Inventario"
        estado.value = data.response.idEstado == 1 ? "Activo" : "Inactivo"
    }, () => {
        LimpiarControles()
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
                <td>${data.response.correo}</td>
                <td>${data.response.idEstado == 1 ? 'Activo' : 'Inactivo'}</td>
            `
    }, () => {

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
                <td>${usuario.correo}</td>
                <td>${usuario.idEstado == 1 ? 'Activo' : 'Inactivo'}</td>
            `
        })
        loader.hide()
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
            confirmButtonColor: '#7a2a1e',
        });
        return
    }

    var hash = encriptar(psw1)
    const url = `${POST_Usuario}${correo}&${username}&${hash}&${role == 'Vendedor' ? 2 : 3}`
    console.log(url)
    POST(url, "Usuario Creado", "Error al crear el usuario", () => {
        Alerta("Confirmado", "El usuario se creo correctamente", "success")
        LimpiarControles()
    })
}

function EditarUsuario() {
    var username = document.getElementById('nombreUsuario').value
    var correo = document.getElementById('txtCorreo').value
    var role = document.getElementById('rolGeneral').value
    var psw1 = document.getElementById('txtPSW1').value
    var psw2 = document.getElementById('txtPSW2').value
    var hash = encriptar(psw1)

    var estado = document.getElementById('estado')
    var url = `${PUT_Usuario}${correo}&${username}&${hash}&${role == 'Vendedor'? 2 : 3}&${estado.value == 'Activo' ? 1 : 2}`

    PUT(url, "Usuario Editado", "Error al editar el usuario", () => {
        Alerta("Confirmado", "El usuario se edito correctamente", "success")
        LimpiarControles()
        document.getElementById('buscarUsername').value = ''
    })
    window.dialogPSW.close()
}

function EliminarUsuario() {
    const username = document.getElementById('buscarUsername').value
    const url = DELETE_Usuario+username

    DELETE(url, "Usuario Eliminado", "Error al eliminar el usuario", () => {
        Alerta("Confirmado", "El usuario se elimino correctamente", "success")
        LimpiarControles()
        document.getElementById('buscarUsername').value = ''
    })
    window.dialogPSW.close()
}

function ActivateControl(value) {
    document.getElementById('nombreUsuario').disabled = value
    document.getElementById('txtCorreo').disabled = value
    document.getElementById('txtPSW1').disabled = value
    document.getElementById('txtPSW2').disabled = value
    document.getElementById('rolGeneral').disabled = value
    document.getElementById('estado').disabled = value
}

function RDACREARINTERNO() {
    OcultarBuscadorUsername()
    ActivateControl(false)
    document.getElementById('btnCrear').disabled = false
    document.getElementById('btnEditar').disabled = true
    document.getElementById('btnEliminar').disabled = true
    document.getElementById('estado').disabled = true
}

function OcultarBuscadorUsername() {
    LimpiarControles()
    document.getElementById('buscarUsername').value = ''
    document.getElementById('buscarUsername').disabled = true
    document.getElementById('buscarUsername').style.display = 'none'
    document.getElementById('lbaBuscador').style.display = 'none'
    document.querySelector('.form-section').style.marginLeft = '-88px'
}

function MostrarBuscadorUsername() {
    LimpiarControles()
    document.getElementById('buscarUsername').value = ''
    document.getElementById('buscarUsername').disabled = false
    document.getElementById('buscarUsername').style.display = ''
    document.getElementById('lbaBuscador').style.display = ''
    document.querySelector('.form-section').style.marginLeft = '-402px'
}

function LimpiarControles() {
    document.getElementById('nombreUsuario').value = ''
    document.getElementById('txtCorreo').value = ''
    document.getElementById('txtPSW1').value = ''
    document.getElementById('txtPSW2').value = ''
}

export { AddEventsFirst, AddEvents, ObtenerUsuarios, OcultarBuscadorUsername }