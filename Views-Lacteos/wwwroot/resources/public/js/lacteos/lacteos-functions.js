import getHTMLLoaderInstance from "../components/singleton.js";
import { AddEvents } from "../facturacion/facturacion-function.js"
import { AddEvents as EventsClientes, ObtenerClientes } from "../cliente/functions-clientes.js"
import { AddEvents as Eventsproveedores, ObtenerProveedores } from "../proveedores/funciones-proveedor.js"
import { AddEvents as EventsCredito } from "../credito/credito-functions.js"
import { AddEvents as EventsAcercaDe } from "../acercaDe/acerca-de-functios.js"
import { AddEvents as EventsCompras } from "../compras/compras-functions.js"
import { AddEvents as EventsInventario } from '../inventario/inventario-funtions.js'
import { AddEventsFirst, AddEvents as EventsListaUsuario, OcultarBuscadorUsername, ObtenerUsuarios } from '../usuarios/usuarios-functios.js'
import { AddEvents as EventsReportes, RedirectURL } from '../reporte/reporte-functions.js'
import { AddEvents as EventsMantenimiento } from '../mantenimiento/Mantenimiento-functions.js'
import { preventSourceCode } from "../security.js"
import Loader from "../components/loading.js"
import { Cliente_vendedor, clientes_admin } from "../endpoints.js";

const loader = new Loader()
let title = document.getElementById("lbaTituloHeader")
let pantalla = ""
let cargarHome = true

function AddEvent() {
    loader.show()
    camposSlider()
    let toggle = document.querySelector(".toggle");
    toggle.onclick = function () {
        Menutoggle()
    }

    // preventSourceCode();

    if(cargarHome) {
        CargarHome()
        cargarHome = false
    }
    document.getElementById('btnFacturacion').addEventListener('click', () => { 
        pantalla = "/resources/views/facturacion.html"
        title.innerHTML = "Facturación"
        CargarPantallas(() => { AddEvents() })
    })
    document.getElementById('btnProveedores').addEventListener('click', () => {
        pantalla = "/resources/views/proveedores.html"
        title.innerHTML = "Gestión de proveedores"
        CargarPantallas(() => {
            Eventsproveedores()
            ObtenerProveedores()
            document.getElementById('Busquedacedula').style.display = 'none'
            document.getElementById('lbaBuscador').style.display = 'none'
        })
    })
    document.getElementById('btnClientes').addEventListener('click', () => { 
        pantalla = "/resources/views/clientes.html"
        title.innerHTML = "Gestión de clientes"
        CargarPantallas(() => { 
            EventsClientes()
            ObtenerClientes()
            document.getElementById('Busquedacedula').style.display ='none'
            document.getElementById('lbBuscador').style.display = 'none'
            document.querySelector('.buttons').style.marginTop = '40px'

            var role = localStorage.getItem('UsuarioRole');

            document.getElementById('manual1').src = role == 'Admin' ? clientes_admin : Cliente_vendedor
        })
    })
    document.getElementById('btnCreditos').addEventListener('click', () => { 
        pantalla = "/resources/views/credito.html"
        title.innerHTML = "Gestión de créditos"
        CargarPantallas(() => { 
            EventsCredito()
        })
    })
    document.getElementById('btnAcercaDe').addEventListener('click', () => { 
        pantalla = "/resources/views/acerca-de.html"
        title.innerHTML = "Acerca de"
        CargarPantallas(() => {
            EventsAcercaDe()
        })
    })
    document.getElementById('btnCompras').addEventListener('click', () => { 
        pantalla = "/resources/views/compras.html"
        title.innerHTML = "Compras e Inventario"
        CargarPantallas(() => {
            EventsCompras()
            document.getElementById('btnInventario').addEventListener('click', () => { 
                pantalla = "/resources/views/inventario.html"
                title.innerHTML = "Inventario"
                CargarPantallas(() => {
                    EventsInventario()
                    document.getElementById('Busquedacedula').style.display = 'none'
                    document.getElementById('lbBuscador').style.display = 'none'
                    document.getElementById('btnCompra').addEventListener('click', () => {
                        pantalla = "/resources/views/compras.html"
                        title.innerHTML = "Compras e Inventario"
                        CargarPantallas(() => {
                            EventsCompras()
                            AddEvent()
                        })
                    })
                })
            })
        })
    })
    
    document.getElementById('btnUsuarios').addEventListener('click', () => { 
        pantalla = "/resources/views/usuarios.html"
        title.innerHTML = "Gestión de Usuarios"
        CargarPantallas(() => {
            AddEventsFirst()
            OcultarBuscadorUsername()
            document.getElementById('estado').disabled = true
            document.getElementById('btnUsuarioLista').addEventListener('click', () => {
                pantalla = "/resources/views/usuarioLista.html"
                title.innerHTML = "Gestión de Usuarios"
                CargarPantallas(() => {
                    EventsListaUsuario()
                    ObtenerUsuarios()
                })
            })
        })
    })
    document.getElementById('btnReportes').addEventListener('click', () => { 
        pantalla = "/resources/views/reportes.html"
        title.innerHTML = "Reportes"
        CargarPantallas(() => {
            EventsReportes()
            RedirectURL()
        })
    })
    document.getElementById('btnMantenimiento').addEventListener('click', () => { 
        pantalla = "/resources/views/mantenimiento.html"
        title.innerHTML = "Mantenimiento"
        CargarPantallas(() => {
            EventsMantenimiento()
        })
    })
    document.getElementById('btnHome').addEventListener('click', () => { 
        cargarHome()
    })
}

async function CargarHome() {
    loader.show()
    title.innerHTML = "Inicio"
    const content = await new Promise((resolve, reject) => {
        fetch('/resources/views/home.html')
            .then((response) => response.text())
            .then((data => resolve(data)))
            .catch((error) => reject(error));
    })

    document.getElementById('content').innerHTML = content

    loader.hide()
}

async function CargarPantallas(callback) {
    loader.show()
    
    const content = await new Promise((resolve, reject) => {
        fetch(pantalla)
            .then((response) => response.text())
            .then((data => resolve(data))) 
            .catch((error) => reject(error));
    })

    document.getElementById('content').innerHTML = content

    let toggle = document.querySelector(".toggle");
    toggle.onclick = function () {
        Menutoggle()
    }
    callback()
    loader.hide()
}

function Menutoggle() {
    let navigation = document.querySelector(".navigation");
    let main = document.querySelector(".main");
    let topbar = document.querySelector(".topbar");
    let logoIcon = document.querySelector(".logo-icon");

    navigation.classList.toggle("active");
    main.classList.toggle("active");
    topbar.classList.toggle("active");
    logoIcon.classList.toggle("small");
}

async function cargarSidebar() {
    const content = await getHTMLLoaderInstance('/resources/views/sidebar.html')
    document.getElementById('contents').innerHTML = content
    camposSlider()
    document.getElementById('btnCerrar').addEventListener('click', () => {
        localStorage.setItem('UsuarioRole', '')
    })
}

function camposSlider() {
    const usuario = localStorage.getItem('UsuarioRole')
    console.log(usuario)
    
    if (usuario == "Admin") {
            document.getElementById('btnClientes').style.display = "";
            document.getElementById('btnProveedores').style.display = "";
            document.getElementById('btnUsuarios').style.display = "";
            document.getElementById('btnCreditos').style.display = "none";
            document.getElementById('btnCompras').style.display = "none";
            document.getElementById('btnReportes').style.display = "block";
            document.getElementById('btnFacturacion').style.display = "none";
        } else if (usuario == "Vendedor") {
            document.getElementById('btnFacturacion').style.display = "";
            document.getElementById('btnClientes').style.display = "";
            document.getElementById('btnCreditos').style.display = "";
            document.getElementById('btnCompras').style.display = "none";
            document.getElementById('btnProveedores').style.display = "none";
            document.getElementById('btnUsuarios').style.display = "none";
            document.getElementById('btnReportes').style.display = "none";
            document.getElementById('btnMantenimiento').style.display = "none";
        } else if (usuario == "Encargado de Inventario")  {
            document.getElementById('btnProveedores').style.display = "";
            document.getElementById('btnCompras').style.display = "";
            document.getElementById('btnClientes').style.display = "none";
            document.getElementById('btnCreditos').style.display = "none";
            document.getElementById('btnUsuarios').style.display = "none";
            document.getElementById('btnFacturacion').style.display = "none";
            document.getElementById('btnReportes').style.display = "none";
            document.getElementById('btnMantenimiento').style.display = "none";
        }  else if (usuario == '') {
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

export { AddEvent, cargarSidebar, camposSlider }