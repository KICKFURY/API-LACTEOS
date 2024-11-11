import { GET } from "../generic-functions.js"

function AddEvents() {
    let toggle = document.querySelector(".toggle");
    toggle.onclick = function () {
        Menutoggle()
    }
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

function cargarSidebar() {
    GET('/resources/views/slider.html', "No se pudo cargar el contenido", 2, (data) => {
        const contents = document.getElementById('contents')

        if (contents) {
            contents.innerHTML = data;
            camposSlider()
            document.getElementById('btnCerrar').addEventListener('click', () => {
                localStorage.setItem('UsuarioRole', "")
            })
        } else {
            console.error("El contenedor 'contents' no está presente en el DOM.");
        }
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
        }  
}

export { AddEvents, cargarSidebar, camposSlider }