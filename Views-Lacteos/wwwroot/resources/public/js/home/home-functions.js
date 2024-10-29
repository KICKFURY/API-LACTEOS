
function RedirectURL() {
    const usuario = localStorage.getItem('UsuarioRole')
    if (usuario == "") {
        alert("Primero debe iniciar sesión")
        window.open('/index.html', '_self')
    }
}

export { RedirectURL }