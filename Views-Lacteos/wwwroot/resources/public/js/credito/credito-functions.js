import { GET, PUT } from "../generic-functions.js";
import { GET_FacturaYDetalles, GET_ClienteById, GET_CreditoByIdVenta, GET_Creditos, GET_VentaById, PUT_Credito, credito_vendedor } from "../endpoints.js";
import { Alerta } from "../components/alert.js";
import { credito, creditoVista } from "./credito-objects.js";

let idVenta = 0
let totalVenta = 0
let cliente = 0
let saldoPendiente = 0
let plazo = 0
let realizarcambio = false 
let fechaPago = ""


function AddEvents() {
    document.getElementById("txtVendedor").value =
        localStorage.getItem("vendedor");
    document.getElementById("txtNumeroFactura").addEventListener("keyup", () => {
        GetFacturaCredito();
    });

    document.getElementById('btnGuardar').addEventListener("click", ActualizarCredito)

    document.getElementById('btnManual').addEventListener('click', () => {
        document.getElementById('manual1').src = credito_vendedor
        window.manual.showModal()
    })

    document.getElementById('manual2').addEventListener('click', () => {
        window.manual.close()
    })


    GetCreditos();
}
function GetFacturaCredito() {
    const numeroFactura = document.getElementById("txtNumeroFactura").value;
    GET(GET_FacturaYDetalles + numeroFactura, "Error al traer la factura", 1, (data) => {
        if (data.response.tipoVenta == "Credito") {
            console.log(data.response.idCliente)
            cliente = parseInt(data.response.idCliente);
            totalVenta = data.response.totalVenta;
            idVenta = data.response.id;
            GetDetallesCredito();
            GetCliente();
        }
    }, () => {}
    );
}
function GetCliente() {
    GET(GET_ClienteById + cliente, "Error al traer el cliente", 1, (data) => {
        document.getElementById("txtCliente").value = `${data.response.nombreCliente} ${data.response.apellidoCliente}`;
    }, () => {});
}
function GetDetallesCredito() {
    GET(GET_CreditoByIdVenta + idVenta, "Error al cargar credito", 1, (data) => {
        plazo = data.response.plazo;
        saldoPendiente = data.response.totalPago;
        document.getElementById("txtTotal").value = totalVenta;
        document.getElementById("txtPlazo").value = plazo;
        document.getElementById("txtCancelar").value = parseInt(totalVenta) / parseInt(plazo);
        document.getElementById("txtSaldo").value = saldoPendiente == 0 ? saldoPendiente : saldoPendiente - parseInt(totalVenta) / parseInt(plazo);
        realizarcambio = true    
    },() => {});
}
async function GetCreditos() {
    const tbody = document.querySelector("table tbody");
    tbody.innerHTML = "";
    try {
        const creditosResponse = await fetchCreditos();
        for (const credito of creditosResponse) {
        const ventaInfo = await GetVentas(credito.idVenta);
        const tr = document.createElement("tr");
        tr.innerHTML = `
            <td>${ventaInfo.numeroFactura}</td>
            <td>${ventaInfo.fechaVenta.split('T')[0]}</td>
            <td>${ventaInfo.totalVenta}</td>
            <td>${credito.plazo}</td>
            <td>${credito.totalPago}</td>
            <td>${credito.fechaPago.split('T')[0]}</td>
            <td>${credito.totalPago == 0 ? "Pagado" : "Pendiente"}</td>
        `;
        tbody.appendChild(tr);
        }
    } catch (error) {
        console.error("Error obteniendo los créditos o ventas:", error);
    }
}
async function fetchCreditos() {
    return new Promise((resolve, reject) => {
        GET(GET_Creditos, "Erro al cargar los creditos", 1, (data) => {
            resolve(data.response);
        }, (error) => {
            reject(error);
        });
    });
}
async function GetVentas(idVenta) {
    return new Promise((resolve, reject) => {
        GET(`${GET_VentaById}${idVenta}`, "Error al cargar la venta", 1, (data) => {
            resolve(data.response);
        }, (error) => {
            reject(error);
        });
    });
}
function ActualizarCredito() {

    if (!realizarcambio) {
        Alerta("Error", "Ingrese el numero de factura", "error");
    }

    PUT(`${PUT_Credito}${idVenta}&${parseFloat(document.getElementById("txtSaldo").value)}&${document.getElementById("fechaDiaPago").value}`, "Credito Editado", "Error al editar el credito", () => {
        GetCreditos();
        Alerta("Éxito", "El crédito ha sido editado", "success");
        realizarcambio = false
        LimpiarCampos();
    })
}
function LimpiarCampos (){
    document.getElementById("txtCliente").value = ''
    document.getElementById("txtNumeroFactura").value = ''
    document.getElementById("txtTotal").value = ''
    document.getElementById("txtSaldo").value = ''
    document.getElementById("txtPlazo").value = ''
    document.getElementById("txtCancelar").value = ''
}

export { AddEvents };