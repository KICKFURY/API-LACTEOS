import { GET, PUT } from "../generic-functions.js";
import { GET_FacturaYDetalles, GET_ClienteById, GET_CreditoByIdVenta, GET_Creditos, GET_VentaById, PUT_Credito, credito_vendedor } from "../endpoints.js";
import { Alerta } from "../components/alert.js";
import { credito, creditoVista } from "./credito-objects.js";
import Loader from '../components/loading.js'

const loader = new Loader()

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
            credito.cliente = parseInt(data.response.idCliente);
            credito.totalVenta = data.response.totalVenta;
            credito.idVenta = data.response.id;
            GetDetallesCredito();
            GetCliente();
        }
    }, () => {}
    );
}
function GetCliente() {
    GET(GET_ClienteById + credito.cliente, "Error al traer el cliente", 1, (data) => {
        creditoVista.cliente.value = `${data.response.nombreCliente} ${data.response.apellidoCliente}`;
    }, () => {});
}
function GetDetallesCredito() {
    GET(GET_CreditoByIdVenta + credito.idVenta, "Error al cargar credito", 1, (data) => {
        credito.plazo = data.response.plazo;
        credito.saldoPendiente = data.response.totalPago;
        creditoVista.total.value = credito.totalVenta;
        creditoVista.plazo.value = credito.plazo;
        creditoVista.cancelar.value = parseInt(credito.totalVenta) / parseInt(credito.plazo);
        creditoVista.saldo.value = credito.saldoPendiente == 0 ? credito.saldoPendiente : credito.saldoPendiente - parseInt(credito.totalVenta) / parseInt(credito.plazo);
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
        loader.hide();
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
    PUT(`${PUT_Credito}${credito.idVenta}&${parseFloat(creditoVista.saldo.value)}&${creditoVista.fecha.value}`, "Credito Editado", "Error al editar el credito", () => {
        GetCreditos();
        Alerta("Éxito", "El crédito ha sido editado", "success");
        LimpiarCampos();
    })
}
function LimpiarCampos (){
    creditoVista.cliente.value = ''
    creditoVista.numeroFactura.value = ''
    creditoVista.total.value = ''
    creditoVista.saldo.value = ''
    creditoVista.plazo.value = ''
    creditoVista.cancelar.value = ''
}

export { AddEvents };