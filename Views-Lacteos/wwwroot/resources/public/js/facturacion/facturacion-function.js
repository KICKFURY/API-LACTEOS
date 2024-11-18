import { POST, GET } from "../generic-functions.js";
import { 
    POST_Factura, POST_DetallesFactura, REPORTE_Factura, GET_Productos, GET_Producto,
    GET_Cliente
} from "../endpoints.js";

let productos = [];
let total = 0;

function AddEvents() {
    document.getElementById('cmbProductos').addEventListener("change", CargarProducto)
    document.getElementById('cmbProductos').addEventListener("input", CargarProducto)
    document.getElementById('btnAgregar').addEventListener('click', agregarProducto);
    document.getElementById('btnFacturar').addEventListener('click', facturar)
    document.getElementById('txtCedulaCliente').addEventListener("keyup", () => {
        const proveedor = document.getElementById('txtCedulaCliente').value;

        if (proveedor.length == 14) {
            CargarCliente()
        } else {
            document.getElementById('txtProveedor').value = ''
        }
    })

    CargarProductos()
}

function CargarCliente() {
    const cliente = document.getElementById('txtCedulaCliente').value;
    let nombre = document.getElementById('nombreCliente');
    const url = GET_Cliente + cliente;

    GET(url, "Error al cargar el producto", 1, (data) => {
        nombre.value = data.response.nombreCliente + " " + data.response.apellidoCliente
    }, () => {
        Alerta("Error", "No se encontro el proveedor", "error")
        document.getElementById('txtCedulaCliente').value = '';
    })
}

function CargarProducto() {
    const producto = document.getElementById('cmbProductos').value;
    let stock = document.getElementById('txtStock');
    let precio = document.getElementById('txtPrecio');
    const url = GET_Producto + producto;

    GET(url, "Error al cargar el producto", 1, (data) => {
        stock.value = data.response.cantidadProducto
        precio.value = data.response.precioProducto
    })
}

function agregarProducto() {
    const producto = document.getElementById('cmbProductos').value;
    const cantidad = parseInt(document.getElementById('txtCantidad').value);
    const precio = parseInt(document.getElementById('txtPrecio').value);
    const subtotal = cantidad * precio;

    if (!producto || isNaN(cantidad) || isNaN(precio)) {
        Swal.fire({
            title: 'Error',
            text: 'Por favor, complete todos los campos correctamente',
            icon: 'error',
            confirmButtonText: 'Aceptar',
            confirmButtonColor: '#7a2a1e',
        });
        return;
    }

    productos.push({ producto, cantidad, precio, subtotal });
    actualizarTablaProductos();
    calcularTotal();
    limpiarCamposProducto();
}

function CargarProductos() {
    const selectElement = document.getElementById('cmbProductos')
    GET(GET_Productos, "Error al cargar los productos", 1, (data) => {
        data.response.forEach(producto => {
            let optionElement = document.createElement('option')
            optionElement.value = producto.nombreProducto
            optionElement.textContent = producto.nombreProducto
            selectElement.appendChild(optionElement)
        })
    })
}

function actualizarTablaProductos() {
    const tbody = document.querySelector('table tbody');
    tbody.innerHTML = '';

    productos.forEach((item, index) => {
        const tr = document.createElement('tr');
        tr.innerHTML = `
            <td>${item.producto}</td>
            <td>${item.cantidad}</td>
            <td>$${item.precio.toFixed(2)}</td>
            <td>$${item.subtotal.toFixed(2)}</td>
            <td><button id="btnEditar">Editar</button></td>
            <td><button id="btnEliminar">Eliminar</button></td>
            `;
        tbody.appendChild(tr);
        document.getElementById('btnEditar').addEventListener('click', ()=>{
            editarProducto(index)
        });
        document.getElementById('btnEliminar').addEventListener('click', ()=>{
            eliminarProducto(index)
        });
    });
}

function calcularTotal() {
    total = productos.reduce((sum, item) => sum + item.subtotal, 0);
    document.getElementById('txtTotal').value = total.toFixed(2);
}

function limpiarCamposProducto() {
    document.getElementById('cmbProductos').value = '';
    document.getElementById('txtStock').value = '';
    document.getElementById('txtCantidad').value = '';
    document.getElementById('txtPrecio').value = '';
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
    const vendedor = document.getElementById('txtVendedor').value;
    const cedulaCliente = document.getElementById('txtCedulaCliente').value;
    const tipoPago = document.getElementById('cmbTipoPago').value;
    const totalVenta = parseInt(document.getElementById('txtTotal').value);
    const numeroFactura = document.getElementById('txtNumeroFactura').value;

    if (productos.length === 0) {
        Swal.fire({
            title: 'Error',
            text: "No hay productos en la factura",
            icon: 'error',
            confirmButtonText: 'Aceptar',
            confirmButtonColor: '#7a2a1e',
        });
        return;
    }

    try {
        var urlVentas = `${POST_Factura}${cedulaCliente}&${vendedor}&${totalVenta}&${tipoPago}&${numeroFactura}`;
        await new Promise((resolve, reject) => {
            POST(urlVentas, "Factura creada", "Error al crear la factura", resolve, reject);
        })

        for (const items of productos) {
            var urlProducto = `${POST_DetallesFactura}${items.producto}&${items.cantidad}&${items.precio}`
            await new Promise((resolve, reject) => {
                POST(urlProducto, "Dealles de factura creados", "Error al crear los detalles de la factura ", resolve, reject)
            })
        }

        Swal.fire({
            title: 'Confirmado',
            text: "Factura creada exitosamente",
            icon: 'success',
            confirmButtonText: 'Aceptar',
            confirmButtonColor: '#7a2a1e',
        });
        limpiarFormulario();

        document.getElementById('reporteFactura').src = REPORTE_Factura+numeroFactura
        window.reporte.showModal()
        
        document.getElementById('reporte1').addEventListener('click', ()=>{
            window.reporte.close()
        })
    } catch (error) {
        console.error("Error al crear la factura:", error);
        alert("Error al crear la factura. Por favor, intente de nuevo.");
    }
}

function limpiarFormulario() {
    productos = [];
    actualizarTablaProductos();
    calcularTotal();
}

export { AddEvents, CargarProducto }