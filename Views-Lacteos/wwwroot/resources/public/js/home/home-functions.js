function RedirectURL() {
    const usuario = localStorage.getItem('UsuarioRole')
    if (usuario == "") {
        Swal.fire({
            title: 'Acceso denegado',
            text: 'Primero debe iniciar sesión',
            icon: 'error',
            confirmButtonText: 'Aceptar'
        }).then(result => {
            if (result.isConfirmed) {
                window.open('/index.html', '_self')
            }
        })
        
    }
}

export { RedirectURL }