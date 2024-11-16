import { GET, POST } from '../generic-functions.js'
import { GET_Productos, POST_Producto } from '../endpoints.js'
import { Alerta } from '../components/alert.js'

function AddEvents() {
    var btn = document.getElementById('btnGenerarReporte')

    btn.addEventListener('click', () => {
        window.reporte.showModal()
    })
    
    document.getElementById('reporte1').addEventListener('click', ()=>{
        window.reporte.close()
    })
    
    document.getElementById('btnCrear').addEventListener('click', Crearproducto)

    GetProductos()
}

function Crearproducto() {
    let nombre = document.getElementById('txtProducto').value
    let descripcion = document.getElementById('txtDescripción').value
    let cantidad = document.getElementById('txtCantidad').value
    let precio = document.getElementById('txtPrecio').value
    let minimoStock = document.getElementById('txtMínimoStock').value
    let expiracion = document.getElementById('fechaExpiracion').value
    const url = `${POST_Producto}${nombre}&${descripcion}&${precio}&${cantidad}&${minimoStock}&${expiracion}`

    if (nombre === '' || descripcion === '' || precio === '' || minimoStock === '' || expiracion === '') {
        Alerta("Error", "Todos los campos son requeridos", "error")
        return
    }

    if (isNaN(precio) || isNaN(cantidad) || isNaN(minimoStock)) {
        Alerta("Error", "Los valores ingresados deben ser numéricos", "error")
        return
    }

    POST(url, "Producto Creado correctamente", "Error al crear el producto", () => {
        Alerta("Confirmado", "Producto Creado correctamente", "success")
    })
}

function GetProductos() {
    GET(GET_Productos, "Error al cargar los productos", 1, (data) => {
        const productosTabla = document.getElementById('tablaProductos').getElementsByTagName('tbody')[0]
        productosTabla.innerHTML = ''
        data.response.forEach(productos => {
            let row = productosTabla.insertRow();
            row.innerHTML = `
                <td>${productos.nombreProducto}</td>
                <td>${productos.descripcionProducto}</td>
                <td>${productos.precioProducto}</td>
                <td>${productos.cantidadProducto}</td>
            `
        })
    }, () => {});
}

export { AddEvents }