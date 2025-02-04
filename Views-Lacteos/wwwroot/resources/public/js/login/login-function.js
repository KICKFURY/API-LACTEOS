import { GET, GET_SERVIDOR } from "../generic-functions.js" 
import { encriptar } from "../security.js"
import { GET_Login } from "../endpoints.js"
import Loader from '../components/loading.js'
import { preventSourceCode } from "../security.js"

const loader = new Loader()

function AddEvents() {
    document.getElementById('btnIniciar').addEventListener('click', Login)
    
    preventSourceCode()
    loader.hide()
}

function Login() {
    var username = document.getElementById('txtUsername').value
    var password = document.getElementById('txtPassword').value
    var url = `${GET_Login}${username}`
    var psw = encriptar(password)

    GET(url, "Usuario no encontrado", 1, (data) => {
        console.log(data.response)
        localStorage.setItem('UsuarioRole', data.response.idRole == 1 ? "Admin" : data.response.idRole == 2 ? "Vendedor" : "Encargado de Inventario")
        localStorage.setItem('UserPSW', data.response.contra)
        localStorage.setItem('vendedor', data.response.nombreUsuario)
        if (data.response.nombreUsuario == username && data.response.contra == psw) {
            window.open('/resources/views/lacteos.html', '_self')
        } else {
            Swal.fire({
                title: 'Error',
                text: 'La contraseña o el usuario es incorrecta',
                icon: 'error',
                confirmButtonText: 'Aceptar',
                confirmButtonColor: '#7a2a1e',
            });
        }
    }, (error) => {
        console.log(error);
    })

    // GET_SERVIDOR(`https://lacteos-la-granja.great-site.net/api/login.php?param=${username}`, 'Usuario no encontrado', (data) => {
    //     data.forEach(usuario => {
    //     localStorage.setItem('UsuarioRole', usuario.idRole == 1 ? "Admin" : usuario.idRole == 2 ? "Vendedor" : "Encargado de Inventario")
    //         if (usuario.nombreUsuario == username && usuario.contra == psw) {
    //             window.open('/resources/views/home.html', '_self')
    //         }
    //     })
    // })
}

export { AddEvents }