import { POST, GET } from "../generic-functions.js";
import { 
    POST_Factura, POST_DetallesFactura, REPORTE_Factura, GET_Productos, GET_Producto,
    GET_Cliente, GET_NumeroFactura, POST_Credito
} from "../endpoints.js";
import { factura } from './factura-object.js'
import { Alerta } from '../components/alert.js'
import Loader from '../components/loading.js'

const loader = new Loader()

let productos = [];
let total = 0;

function AddEvents() {
    const hoy = new Date()
    const dia = String(hoy.getDate()).padStart(2, '0')
    const mes = String(hoy.getMonth() + 1).padStart(2, '0');
    const año = hoy.getFullYear()
    const fechaFormateada = `${año}-${mes}-${dia}`
    document.getElementById('fechaVenta').value = fechaFormateada

    factura.producto.addEventListener("change", CargarProducto)
    factura.producto.addEventListener("input", CargarProducto)
    factura.tipoPago.addEventListener("change", ActivarPlazos)
    factura.tipoPago.addEventListener("input", ActivarPlazos)
    document.getElementById('btnAgregar').addEventListener('click', agregarProducto);
    document.getElementById('btnFacturar').addEventListener('click', facturar)
    factura.cedulaCliente.addEventListener("keyup", () => {
        if (factura.cedulaCliente.value.length == 14) {
            CargarCliente()
        } else {
            document.getElementById('nombreCliente').value = ''
        }
    })

    CargarProductos()
    factura.vendedor.value = localStorage.getItem('vendedor')
    CargarNumeroUltimaVenta()
    loader.hide()
}

function ActivarPlazos() {
    if (factura.tipoPago.value === 'Credito') {
        document.getElementById('txtPlazo').disabled = false
    } else {
        document.getElementById('txtPlazo').disabled = true
    }
}

function CargarNumeroUltimaVenta() {
    GET(GET_NumeroFactura, "Error al cargar el numero de la venta", 1, (data) => {
        factura.numeroFactura.value = data.response.numeroFactura != null ?  parseInt(data.response.numeroFactura) + 1 : 1
    })
}

function CargarCliente() {
    let nombre = document.getElementById('nombreCliente');
    const url = GET_Cliente + factura.cedulaCliente.value;

    GET(url, "Error al cargar el producto", 1, (data) => {
        nombre.value = data.response.nombreCliente + " " + data.response.apellidoCliente
    }, () => {
        Alerta("Error", "No se encontro el cliente", "error")
        factura.cedulaCliente.value = '';
    })
}

function CargarProducto() {
    const url = `${GET_Producto}${factura.producto.value}`;

    GET(url, "Error al cargar el producto", 1, (data) => {
        factura.precio.value= data.response.precioProducto
    }, () => {})
}

function agregarProducto() {
    const producto = factura.producto.value
    const cantidad = parseInt(factura.cantidad.value)
    const precio = parseInt(factura.precio.value)

    const subtotalIVA = (cantidad * precio) * 1.15
    const subTotalSinIVA = subtotalIVA - precio * cantidad


    if (!producto || isNaN(cantidad) || isNaN(precio)) {
        Alerta("Error", "Por favor, complete todos los campos correctamente", "error")
        return;
    }

    productos.push({ producto, cantidad, precio, subtotalIVA, subTotalSinIVA });
    actualizarTablaProductos();
    calcularTotal();
    limpiarCamposProducto();
}

function CargarProductos() {
    GET(GET_Productos, "Error al cargar los productos", 1, (data) => {
        data.response.forEach(producto => {
            let optionElement = document.createElement('option')
            optionElement.value = producto.nombreProducto
            optionElement.textContent = producto.nombreProducto
            optionElement.disabled = producto.cantidadProducto <= producto.minimoStockProducto ? true : false
            factura.producto.appendChild(optionElement)
            CargarProducto()
        })
    }, () => {})
}

function actualizarTablaProductos() {
    const tbody = document.querySelector('table tbody');
    tbody.innerHTML = '';

    productos.forEach((item, index) => {
        const tr = document.createElement('tr');
        tr.innerHTML = `
            <td>${item.producto}</td>
            <td>${item.cantidad}</td>
            <td>C$${item.precio.toFixed(2)}</td>
            <td>C$${item.subTotalSinIVA.toFixed(2)}</td>
            <td>C$${item.subtotalIVA.toFixed(2)}</td>
            <td><button id="btnEditar${index}">Editar</button></td>
            <td><button id="btnEliminar${index}">Eliminar</button></td>
            `;
        tbody.appendChild(tr);
        document.getElementById(`btnEditar${index}`).addEventListener('click', ()=>{
            editarProducto(index)
        });
        document.getElementById(`btnEliminar${index}`).addEventListener('click', ()=>{
            eliminarProducto(index)
        });
    });
}

function calcularTotal() {
    total = productos.reduce((sum, item) => sum + item.subtotalIVA, 0);
    document.getElementById('txtTotal').value = total.toFixed(2);
}

function limpiarCamposProducto() {
    factura.cantidad.value = '';
}

function editarProducto(index) {
    const item = productos[index];
    factura.producto.value = item.producto;
    factura.cantidad.value = item.cantidad;
    factura.precio.value = item.precio;
    productos.splice(index, 1);
    actualizarTablaProductos();
    calcularTotal();
}

function eliminarProducto(index) {
    productos.splice(index, 1);
    actualizarTablaProductos();
    calcularTotal();
}

async function facturar() {
    if (productos.length === 0) {
        Alerta("Error", "No hay productos en la factura", "error")
        return;
    }

    if (document.getElementById('nombreCliente').value == '') {
        Alerta("Error", "Por favor, ingrese el nombre del cliente", "error")
        return;
    }

    try {
        var urlVentas = `${POST_Factura}${factura.cedulaCliente.value}&${factura.vendedor.value}&${parseInt(factura.totalVenta.value)}&${factura.tipoPago.value}&${factura.numeroFactura.value}`;
        await new Promise((resolve, reject) => {
            POST(urlVentas, "Factura creada", "Error al crear la factura", resolve, reject);
        })

        for (const items of productos) {
            var urlProducto = `${POST_DetallesFactura}${items.producto}&${items.cantidad}&${items.precio}`
            await new Promise((resolve, reject) => {
                POST(urlProducto, "Dealles de factura creados", "Error al crear los detalles de la factura ", resolve, reject)
            })
        }

        if (factura.tipoPago.value == "Credito") {
            var t = factura.totalVenta.value.split(".")
            var url = `${POST_Credito}${t[0]}&${factura.plazo.value}&${factura.fecha.value}`
            await new Promise((resolve, reject) => {
                POST(url, "Crédito creado", "Error al crear el crédito", resolve, reject)
            })
        }

        Alerta("Confirmado", "Factura creada exitosamente", "success",)
        limpiarFormulario();

        document.getElementById('reporteFactura').src = REPORTE_Factura+factura.numeroFactura.value
        window.reporte.showModal()
        
        document.getElementById('reporte1').addEventListener('click', () => {
            window.reporte.close()
            document.getElementById('reporteFactura').src = ''
        })
        CargarNumeroUltimaVenta()
    } catch (error) {
        console.error("Error al crear la factura:", error);
        Alerta("Error", "Error al crear la factura. Por favor, intente de nuevo.", "error")
    }
}

function limpiarFormulario() {
    productos = [];
    actualizarTablaProductos();
    calcularTotal();
    factura.cedulaCliente.value = ''
    document.getElementById('nombreCliente').value = ''
}

export { AddEvents, CargarProducto }