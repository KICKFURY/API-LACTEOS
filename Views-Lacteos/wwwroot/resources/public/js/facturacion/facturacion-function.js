import { POST } from "../generic-functions.js";
import { POST_Factura } from "../endpoints.js";

let productos = [];
let total = 0;

function agregarProducto() {
    const producto = document.getElementById('txtProducto').value;
    const cantidad = parseInt(document.getElementById('txtCantidad').value);
    const precio = parseInt(document.getElementById('txtPrecio').value);
    const subtotal = cantidad * precio;

    if (!producto || isNaN(cantidad) || isNaN(precio)) {
        alert('Por favor, complete todos los campos correctamente.');
        return;
    }

    productos.push({ producto, cantidad, precio, subtotal });
    actualizarTablaProductos();
    calcularTotal();
    limpiarCamposProducto();
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
    document.getElementById('txtProducto').value = '';
    document.getElementById('txtStock').value = '';
    document.getElementById('txtCantidad').value = '';
    document.getElementById('txtPrecio').value = '';
}

function editarProducto(index) {
    const item = productos[index];
    document.getElementById('txtProducto').value = item.producto;
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

    var url = `${POST_Factura}${cedulaCliente}&${vendedor}&${totalVenta}&${tipoPago}&${numeroFactura}&`;
    
    productos.forEach(items => {
        url += `${items.producto}&${items.cantidad}&${items.precio}`
    })
    console.log(url)
    POST(url, "Factura creada", "Error al crear la factura", () => {
        limpiarFormulario()
    })
}

function limpiarFormulario() {
    document.querySelector('form').reset();
    productos = [];
    actualizarTablaProductos();
    calcularTotal();
}

function AddEvents() {
    document.getElementById('btnAgregar').addEventListener('click', agregarProducto);
    document.getElementById('btnFacturar').addEventListener('click', facturar)
}

export { AddEvents }