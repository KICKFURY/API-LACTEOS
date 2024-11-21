import { Alerta } from "../components/alert.js";
import Loader from "../components/loading.js"
import { REPORTE_Clientes, REPORTE_Productos, REPORTE_Proveedores, REPORTE_Usuarios, reportes_admin, REPORTE_ArqueoDelDia } from "../endpoints.js";

const loader = new Loader()

function AddEvents() {
    document.getElementById('btnManual').addEventListener("click", () => {
        document.getElementById('rdlc').src = reportes_admin
        window.reporte.showModal()
    })
    document.getElementById('btnReporteCliente').addEventListener("click", () => {
        document.getElementById('rdlc').src = REPORTE_Clientes
        window.reporte.showModal()
    })
    document.getElementById('btnReporteInventario').addEventListener("click", () => {
        document.getElementById('rdlc').src = REPORTE_Productos
        window.reporte.showModal()
    })
    document.getElementById('btnReporteProveedores').addEventListener("click", () => {
        document.getElementById('rdlc').src = REPORTE_Proveedores
        window.reporte.showModal()
    })
    document.getElementById('btnReporteUsuario').addEventListener("click", () => {
        document.getElementById('rdlc').src = REPORTE_Usuarios
        window.reporte.showModal()
    })
    document.getElementById('btnReporteArqueo').addEventListener("click", () => {
        document.getElementById('rdlc').src = REPORTE_ArqueoDelDia
        window.reporte.showModal()
    })

    document.getElementById('reporte1').addEventListener('click', () => {
        window.reporte.close()
        document.getElementById('rdlc').src = ''
    })


    // window.addEventListener('unload', function() { 
    //     alert('El usuario ha aceptado salir.');
    //     localStorage.setItem("UsuarioRole", "");
    // });
    loader.hide();
}

function RedirectURL() {
    const usuario = localStorage.getItem("UsuarioRole");
    if (usuario == "") {
        Alerta("Error", "Primero debe iniciar sesión", "Error");
        window.open("/index.html", "_self");
    }
}

export { AddEvents, RedirectURL };
