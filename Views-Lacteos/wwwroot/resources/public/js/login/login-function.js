import { GET } from "../generic-functions.js" 
import { encriptar } from "../security.js"
import { GET_Login } from "../endpoints.js"

function AddEvents() {
    document.getElementById('btnIniciar').addEventListener('click', Login)
}

function Login() {
    var username = document.getElementById('txtUsername').value
    var password = document.getElementById('txtPassword').value
    var url = `${GET_Login}${username}`
    var psw = encriptar(password)

    GET(url, "Usuario no encontrado", 1, (data) => {
        localStorage.setItem('UsuarioRole', data.response.idRole == 1 ? "Admin" : data.response.idRole == 2 ? "Vendedor" : "Encargado de Inventario")
        if (data.response.nombreUsuario == username && data.response.contra == psw) {
            window.open('/resources/views/home.html', '_self')
        }
    })
}

export { AddEvents }