import { GET } from '../generic-functions.js';
import { GET_FacturaYDetalles } from '../endpoints.js'
import { Alerta } from '../components/alert.js'

const credito = {
    idVenta: "",
    totalDeuda: "",
    saldoPendiente: "",
    plazo: "",
    fechaPago: ""
}

function AddEvents() {
    document.getElementById("txtVendedor").value = localStorage.getItem("vendedor")
    document.getElementById("txtNumeroFactura").addEventListener('keyup', () => {
        GetFacturaCredito()
    })
}

function GetFacturaCredito() {
    const numeroFactura = document.getElementById("txtNumeroFactura").value
    GET(GET_FacturaYDetalles+numeroFactura, "Error al traer la factura", 1, (data) => {
        if (data.response.tipoVenta == "Credito") {

        } else {
            Alerta("Error", "La factura no es de crédito", "error")
            return
        }
    }, () => {})
}

export { AddEvents }