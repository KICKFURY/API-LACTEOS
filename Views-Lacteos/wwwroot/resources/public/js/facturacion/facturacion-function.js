import { POST, GET } from "../generic-functions.js";
import { 
    POST_Factura, POST_DetallesFactura, REPORTE_Factura, GET_Productos, GET_Producto,
    GET_Cliente, GET_NumeroFactura, POST_Credito, Facturacion_vendedor, REPORTE_Credito
} from "../endpoints.js";
import { Alerta } from '../components/alert.js'

let productos = [];
let total = 0;

function AddEvents() {
    const hoy = new Date()
    const dia = String(hoy.getDate()).padStart(2, '0')
    const mes = String(hoy.getMonth() + 1).padStart(2, '0');
    const año = hoy.getFullYear()
    const fechaFormateada = `${año}-${mes}-${dia}`
    document.getElementById('fechaVenta').value = fechaFormateada

    document.getElementById('cmbProductos').addEventListener("change", CargarProducto)
    document.getElementById('cmbProductos').addEventListener("input", CargarProducto)
    document.getElementById('cmbTipoPago').addEventListener("change", ActivarPlazos)
    document.getElementById('cmbTipoPago').addEventListener("input", ActivarPlazos)
    document.getElementById('btnAgregar').addEventListener('click', agregarProducto);
    document.getElementById('btnFacturar').addEventListener('click', facturar)
    document.getElementById('txtCedulaCliente').addEventListener("keyup", () => {
        if (document.getElementById('txtCedulaCliente').value.length == 14) {
            CargarCliente()
        } else {
            document.getElementById('nombreCliente').value = ''
        }
    })

    CargarProductos()
    document.getElementById('txtVendedor').value = localStorage.getItem('vendedor')
    CargarNumeroUltimaVenta()

    document.getElementById('btnManual').addEventListener('click', () => {
        document.getElementById('manual1').src = Facturacion_vendedor
        window.manual.showModal()
    })

    document.getElementById('manual2').addEventListener('click', () => {
        window.manual.close()
    })
}

function ActivarPlazos() {
    if (document.getElementById('cmbTipoPago').value === 'Credito') {
        document.getElementById('txtPlazo').disabled = false
    } else {
        document.getElementById('txtPlazo').disabled = true
    }
}

function CargarNumeroUltimaVenta() {
    GET(GET_NumeroFactura, "Error al cargar el numero de la venta", 1, (data) => {
        document.getElementById('txtNumeroFactura').value = data.response.numeroFactura != null ?  parseInt(data.response.numeroFactura) + 1 : 1
    })
}

function CargarCliente() {
    let nombre = document.getElementById('nombreCliente');
    const url = GET_Cliente + document.getElementById('txtCedulaCliente').value;

    GET(url, "Error al cargar el producto", 1, (data) => {
        nombre.value = data.response.nombreCliente + " " + data.response.apellidoCliente
    }, () => {
        Alerta("Error", "No se encontro el cliente", "error")
        document.getElementById('txtCedulaCliente').value = '';
    })
}

function CargarProducto() {
    const url = `${GET_Producto}${document.getElementById('cmbProductos').value}`;

    GET(url, "Error al cargar el producto", 1, (data) => {
        document.getElementById('txtPrecio').value= data.response.precioProducto
    }, () => {})
}

function agregarProducto() {
    const producto = document.getElementById('cmbProductos').value
    const cantidad = parseInt(document.getElementById('txtCantidad').value)
    const precio = parseInt(document.getElementById('txtPrecio').value)

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
            document.getElementById('cmbProductos').appendChild(optionElement)
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
    document.getElementById('txtCantidad').value = '';
}

function editarProducto(index) {
    const item = productos[index];
    document.getElementById('cmbProductos').value = item.producto;
    document.getElementById('txtCantidad').value = item.cantidad;
    document.getElementById('txtPrecio').value = item.precio;
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
        var urlVentas = `${POST_Factura}${document.getElementById('txtCedulaCliente').value}&${document.getElementById('txtVendedor').value}&${parseInt(document.getElementById('txtTotal').value)}&${document.getElementById('cmbTipoPago').value}&${document.getElementById('txtNumeroFactura').value}`;
        await new Promise((resolve, reject) => {
            POST(urlVentas, "Factura creada", "Error al crear la factura", resolve, reject);
        })

        for (const items of productos) {
            var urlProducto = `${POST_DetallesFactura}${items.producto}&${items.cantidad}&${items.precio}`
            await new Promise((resolve, reject) => {
                POST(urlProducto, "Dealles de factura creados", "Error al crear los detalles de la factura ", resolve, reject)
            })
        }

        if (document.getElementById('cmbTipoPago').value == "Credito") {
            document.getElementById('reporteFactura').src = REPORTE_Credito+document.getElementById('txtNumeroFactura').value
            var t = document.getElementById('txtTotal').value.split(".")
            let total = parseInt(t[0]) - (parseInt(t[0]) / parseInt(document.getElementById('txtPlazo').value))
            let fecha = sumarUnMes(document.getElementById('fechaVenta').value)
            var url = `${POST_Credito}${total.toString().split(".")[0]}&${document.getElementById('txtPlazo').value}&${fecha}`
            await new Promise((resolve, reject) => {
                POST(url, "Crédito creado", "Error al crear el crédito", resolve, reject)
            })
        } else if (document.getElementById('cmbTipoPago').value === "Contado") {
            document.getElementById('reporteFactura').src = REPORTE_Factura+document.getElementById('txtNumeroFactura').value
        }

        Alerta("Confirmado", "Factura creada exitosamente", "success",)
        limpiarFormulario();

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

function sumarUnMes(fechaStr) {
    let partes = fechaStr.split('-');
    let año = parseInt(partes[0]);
    let mes = parseInt(partes[1]) - 1;
    let dia = parseInt(partes[2]);

    let fecha = new Date(año, mes, dia);

    fecha.setMonth(fecha.getMonth() + 1);

    let nuevoDia = ("0" + fecha.getDate()).slice(-2);
    let nuevoMes = ("0" + (fecha.getMonth() + 1)).slice(-2);
    let nuevoAño = fecha.getFullYear();

    return `${nuevoAño}-${nuevoMes}-${nuevoDia}`;
}

function limpiarFormulario() {
    productos = [];
    actualizarTablaProductos();
    calcularTotal();
    document.getElementById('txtCedulaCliente').value = ''
    document.getElementById('nombreCliente').value = ''
}

export { AddEvents, CargarProducto }