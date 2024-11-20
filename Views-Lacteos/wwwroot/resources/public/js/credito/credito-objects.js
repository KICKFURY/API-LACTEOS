const credito = {
    idVenta: 0,
    totalVenta: 0,
    cliente: 0,
    saldoPendiente: 0,
    plazo: 0,
    fechaPago: "",
};

const creditoVista = {
    numeroFactura: document.getElementById("txtNumeroFactura"),
    cliente: document.getElementById("txtCliente"),
    total: document.getElementById("txtTotal"),
    plazo: document.getElementById("txtPlazo"),
    cancelar: document.getElementById("txtCancelar"),
    saldo: document.getElementById("txtSaldo"),
    fecha: document.getElementById("fechaDiaPago")
};

export { credito, creditoVista };