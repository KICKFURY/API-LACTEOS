import { menu_admin } from '../endpoints.js'
import Loader from '../components/loading.js'

const loader = new Loader()

function AddEventHome() {
    document.getElementById('btnManual').addEventListener('click', () => {
        document.getElementById('manual1').src = menu_admin
        window.manual.showModal()
    })

    document.getElementById('manual2').addEventListener('click', () => {
        window.manual.close()
    })
    
    loader.hide()
}

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

export { RedirectURL, AddEventHome }