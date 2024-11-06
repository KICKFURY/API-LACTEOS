function AddEvents() {
    document.addEventListener("keydown", function (event) {
        if (
            (event.ctrlKey && event.key === "u") ||
            (event.ctrlKey && event.key === "w")
        ) {
            event.preventDefault();
        } else if (event.key === "F12") {
            console.log(event.key);
            event.preventDefault();
        }
    });

    // window.addEventListener('unload', function() { 
    //     alert('El usuario ha aceptado salir.');
    //     localStorage.setItem("UsuarioRole", "");
    // });

    document.addEventListener('contextmenu', function(event) {
        event.preventDefault(); 
    });
}

function RedirectURL() {
    const usuario = localStorage.getItem("UsuarioRole");
    if (usuario == "") {
        alert("Primero debe iniciar sesión");
        window.open("/index.html", "_self");
    }
}

export { AddEvents, RedirectURL };
