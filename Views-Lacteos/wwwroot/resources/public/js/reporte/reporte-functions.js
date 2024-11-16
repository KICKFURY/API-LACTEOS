import { preventSourceCode } from "../security.js";
import { Alerta } from "../components/alert.js";

function AddEvents() {
    preventSourceCode();

    // window.addEventListener('unload', function() { 
    //     alert('El usuario ha aceptado salir.');
    //     localStorage.setItem("UsuarioRole", "");
    // });
}

function RedirectURL() {
    const usuario = localStorage.getItem("UsuarioRole");
    if (usuario == "") {
        Alerta("Error", "Primero debe iniciar sesión", "Error");
        window.open("/index.html", "_self");
    }
}

export { AddEvents, RedirectURL };
