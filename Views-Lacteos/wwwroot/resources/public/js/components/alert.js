function Alerta(title, message, icon) {
    Swal.fire({
        title: title,
        text: message,
        icon: icon,
        confirmButtonText: 'Aceptar',
        confirmButtonColor: '#7a2a1e',
    });
}

export { Alerta }