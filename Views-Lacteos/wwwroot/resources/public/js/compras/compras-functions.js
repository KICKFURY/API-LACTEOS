import { POST_COMPRA, POST_DetallesCompra, GET_Producto, GET_Proveedor, GET_NumeroCompra, GET_Productos } from "../endpoints.js";
import { GET, POST } from "../generic-functions.js"
import { Alerta } from "../components/alert.js"
import Loader from '../components/loading.js'

const loader = new Loader()

let productos = [];
let total = 0;

function AddEvents() {
    document.getElementById('cmbProducto').addEventListener("change", CargarProducto)
    document.getElementById('cmbProducto').addEventListener("input", CargarProducto)
    document.getElementById('btnAgregar').addEventListener("click", AgregarProducto)
    document.getElementById('txtNoProveedor').addEventListener("keyup", () => {
        const proveedor = document.getElementById('txtNoProveedor').value;

        if (proveedor.length == 14) {
            CargarProveedor()
        } else {
            document.getElementById('txtProveedor').value = ''
        }
    })
    document.getElementById('btnFacturar').addEventListener("click", Comprar)

    CargarNumeroUltimaCompra()

    CargarProductos()
    loader.hide()
}

function CargarProductos() {
    GET(GET_Productos, "Error al cargar los productos", 1, (data) => {
        data.response.forEach(producto => {
            let optionElement = document.createElement('option')
            optionElement.value = producto.nombreProducto
            optionElement.textContent = producto.nombreProducto
            document.getElementById("cmbProducto").appendChild(optionElement)
            CargarProducto()
        })
    }, () => {})
}

function CargarNumeroUltimaCompra() {
    GET(GET_NumeroCompra, "Error al cargar el numero de la compra", 1, (data) => {
        document.getElementById('txtNoCompra').value = parseInt(data.response) + 1;
    })
}

function AgregarProducto() {
    const producto = document.getElementById('cmbProducto').value;
    const cantidad = parseInt(document.getElementById('txtCantidad').value);
    const precio = parseInt(document.getElementById('txtPrecio').value);
    const subtotal = cantidad * precio;
    
    if (!producto || isNaN(cantidad) || isNaN(precio)) {
        Alerta("Error", "Por favor, complete todos los campos correctamente", "error")
        return;
    }

    productos.push({ producto, cantidad, precio, subtotal });
    ActualizarTablaProductos()
    calcularTotal()
    limpiarCamposProducto()
}

function ActualizarTablaProductos() {
    const tbody = document.querySelector('table tbody');
    tbody.innerHTML = '';

    productos.forEach((item, index) => {
        const tr = document.createElement('tr');
        tr.innerHTML = `
            <td>${item.producto}</td>
            <td>${item.cantidad}</td>
            <td>${item.precio.toFixed(2)}</td>
            <td>${item.precio * item.cantidad.toFixed(2)}</td>
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
    })
}

function calcularTotal() {
    total = productos.reduce((sum, item) => sum + item.subtotal, 0);
    document.getElementById('txtTotal').value = total.toFixed(2);
}

function limpiarCamposProducto() {
    document.getElementById('txtCantidad').value = '';
}

function editarProducto(index) {
    const item = productos[index];
    document.getElementById('cmbProducto').value = item.producto;
    document.getElementById('txtCantidad').value = item.cantidad;
    document.getElementById('txtPrecio').value = item.precio;
    productos.splice(index, 1);
    ActualizarTablaProductos();
    calcularTotal();
}

function eliminarProducto(index) {
    productos.splice(index, 1);
    ActualizarTablaProductos();
    calcularTotal();
}

async function Comprar() {
    const vendedor = localStorage.getItem("vendedor")
    const rucProveedor = document.getElementById('txtNoProveedor').value
    const totalCompra = parseInt(document.getElementById('txtTotal').value);
    const numeroFactura = document.getElementById('txtNoCompra').value;

    if (productos.length === 0) {
        Alerta("Error", "No hay productos en la facturar", "error")
        return;
    }

    try {
        var urlCompras = `${POST_COMPRA}${rucProveedor}&${vendedor}&${totalCompra}&${numeroFactura}`
        await new Promise((resolve, reject) => {
            POST(urlCompras, "Compra realizada exitosamente", "Error al realizar la compra", resolve, reject)
        })

        for (const items of productos) {
            var urlProducto = `${POST_DetallesCompra}${items.producto}&${items.cantidad}&${items.precio}`
            await new Promise((resolve, reject) => {
                POST(urlProducto, "Dealles de compra creados", "Error al crear los detalles de la compra ", resolve, reject)
            })
        }

        Alerta("Confirmado", "Compra realizada exitosamente", "success")
        limpiarFormulario()
    } catch (error) {
        console.error("Error al realizar la compra: ", error)
        Alerta("Error", "Error al crear la compra. Por favor, intente de nuevo.", "error")
    }
}

function limpiarFormulario() {
    productos = [];
    ActualizarTablaProductos();
    calcularTotal();
}

function CargarProducto() {
    const producto = document.getElementById('cmbProducto').value;
    let descripcion = document.getElementById('txtDescripcion');
    let precio = document.getElementById('txtPrecio');
    const url = GET_Producto + producto;

    GET(url, "Error al cargar el producto", 1, (data) => {
        descripcion.value = data.response.descripcionProducto
        precio.value = data.response.precioProducto
    })
}

function CargarProveedor() {
    const proveedor = document.getElementById('txtNoProveedor').value;
    let nombre = document.getElementById('txtProveedor');
    const url = GET_Proveedor + proveedor;

    GET(url, "Error al cargar el producto", 1, (data) => {
        nombre.value = data.response.nombreProveedor
    }, () => {
        Alerta("Error", "No se encontro el proveedor", "error")
        document.getElementById('txtNoProveedor').value = '';
    })
}

export { AddEvents }