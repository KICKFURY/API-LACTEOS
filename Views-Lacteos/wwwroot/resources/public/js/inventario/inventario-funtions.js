import { GET, POST, PUT, DELETE } from '../generic-functions.js'
import { GET_Productos, POST_Producto, GET_Producto, PUT_Producto, DELETE_Producto } from '../endpoints.js'
import { Alerta } from '../components/alert.js'
import { productos } from './productos.js'

function AddEvents() {
    document.getElementById('btnCrear').addEventListener('click', Crearproducto)
    document.getElementById('btnEditar').addEventListener('click', Editar)
    document.getElementById('btnEliminar').addEventListener('click', Eliminar)
    document.getElementById('btnBuscar').addEventListener('click', () => {
        GetProductos()
    })

    document.getElementById('rdaCrear').addEventListener('change', () => {
        RDACREARINTERNO()
        DesactivarControles(false)
    })
    document.getElementById('rdaEditar').addEventListener('change', () => {
        MostrarBuscadorRUC()
        DesactivarControles(false)
        productos.nombreProducto.disabled = true
        productos.cantidad.disabled = true
        document.getElementById('btnCrear').disabled = true
        document.getElementById('btnEditar').disabled = false
        document.getElementById('btnEliminar').disabled = true
    })
    document.getElementById('rdaEliminar').addEventListener('change', () => {
        MostrarBuscadorRUC()
        DesactivarControles(true)
        document.getElementById('btnCrear').disabled = true
        document.getElementById('btnEditar').disabled = true
        document.getElementById('btnEliminar').disabled = false
    })

    document.getElementById('Busquedacedula').addEventListener('keyup', () => {
        var cedula = document.getElementById('Busquedacedula').value
        if (cedula.length >= 5) {
            CargarProducto()
        } else {
            LimpiarControles()
        }
    })

    document.getElementById('ruc').addEventListener('keyup', () => {
        var cedula = document.getElementById('ruc').value
        if (cedula.length >= 5) {
            GetProducto()
        } else {
            GetProductos()
        }
    })

    var btn = document.getElementById('btnGenerarReporte')

    var role = localStorage.getItem('UsuarioRole');

    if (role == 'Admin') {
        btn.style.display = ''
    } else if (role == 'Vendedor') {
        btn.style.display = 'none'
    } else if (role == 'Encargado de Inventario') {
        btn.style.display = 'none'
    }

    btn.addEventListener('click', () => {
        window.reporte.showModal()
    })
    document.getElementById('reporte1').addEventListener('click', () => {
        window.reporte.close()
    })
    document.getElementById('btnCrear').addEventListener('click', Crearproducto)

    GetProductos()
}

function CargarProducto() {
    var producto = document.getElementById('Busquedacedula').value

    GET(GET_Producto+producto, "Error al cargar el producto", 1, (data) => {
        productos.nombreProducto.value = data.response.nombreProducto
        productos.descripcion.value = data.response.descripcionProducto
        productos.precio.value = data.response.precioProducto
        productos.cantidad.value = data.response.cantidadProducto
        productos.minimoStock.value = data.response.minimoStockProducto
        productos.expiracion.value = data.response.fechaExpiracionProducto
    })
}

function Crearproducto() {
    const url = `${POST_Producto}${productos.nombreProducto.value}&${productos.descripcion.value}&${productos.precio.value}&${productos.cantidad.value}&${productos.minimoStock.value}&${productos.expiracion.value}`

    if (productos.nombreProducto.value === '' || productos.descripcion.value === '' || productos.precio.value === '' || productos.cantidad.value === '' || productos.minimoStock.value === '' || productos.expiracion.value === '') {
        Alerta("Error", "Todos los campos son requeridos", "error")
        return
    }
    if (isNaN(productos.precio.value) || isNaN(productos.cantidad.value) || isNaN(productos.minimoStock.value)) {
        Alerta("Error", "Los valores ingresados deben ser numéricos", "error")
        return
    }

    POST(url, "Producto Creado correctamente", "Error al crear el producto", () => {
        GetProductos()
        LimpiarControles()
        document.getElementById('Busquedacedula').value = ''
        Alerta("Confirmado", "Producto Creado correctamente", "success")
    })
}

function Editar() {
    const url = `${PUT_Producto}${productos.nombreProducto.value}&${productos.descripcion.value}&${productos.precio.value}&${productos.cantidad.value}&${productos.minimoStock.value}&${productos.expiracion.value}`
    PUT(url, "Producto editado correctamente", "Error al editar el producto", () => {
        GetProductos()
        LimpiarControles()
        document.getElementById('Busquedacedula').value = ''
        Alerta("Confirmado", "Producto editado correctamente", "success")
    })
}

function Eliminar() {
    const url = `${DELETE_Producto}${productos.nombreProducto.value}`
    DELETE(url, "Producto eliminado correctamente", "Error al eliminar el producto", () => {
        GetProductos()
        LimpiarControles()
        document.getElementById('Busquedacedula').value = ''
        Alerta("Confirmado", "Producto eliminado correctamente", "success")
    })
}

function GetProductos() {
    GET(GET_Productos, "Error al cargar los productos", 1, (data) => {
        const tbody = document.querySelector('table tbody')
        tbody.innerHTML = ''

        data.response.forEach(productos => {
            const tr = document.createElement('tr')
            tr.innerHTML = `
                <td>${productos.nombreProducto}</td>
                <td>${productos.descripcionProducto}</td>
                <td>${productos.precioProducto}</td>
                <td>${productos.cantidadProducto}</td>
            `
            tbody.appendChild(tr)
        })
    }, () => {});
}

function GetProducto() {
    var producto = document.getElementById('ruc').value
    GET(GET_Producto+producto, "Error al cargar el producto", 1, (data) => {
        const tbody = document.querySelector('table tbody')
        tbody.innerHTML = ''

        const tr = document.createElement('tr')
        tr.innerHTML = `
            <td>${data.response.nombreProducto}</td>
            <td>${data.response.descripcionProducto}</td>
            <td>${data.response.precioProducto}</td>
            <td>${data.response.cantidadProducto}</td>
        `
        tbody.appendChild(tr)
    }, () => {});
}

function LimpiarControles() {
    productos.nombreProducto.value = ""
    productos.descripcion.value = ""
    productos.precio.value = ""
    productos.cantidad.value = ""
    productos.minimoStock.value = ""
    productos.expiracion.value = ""
}

function DesactivarControles(value) {
    productos.nombreProducto.disabled = value
    productos.descripcion.disabled = value
    productos.precio.disabled = value
    productos.cantidad.disabled = value
    productos.minimoStock.disabled = value
    productos.expiracion.disabled = value
}

function RDACREARINTERNO() {
    OcultarBuscadorRUC()
    document.getElementById('btnCrear').disabled = false
    document.getElementById('btnEditar').disabled = true
    document.getElementById('btnEliminar').disabled = true
}

function MostrarBuscadorRUC() {
    LimpiarControles()
    document.getElementById('Busquedacedula').style.display = ''
    document.getElementById('lbBuscador').style.display = ''
    document.getElementById('Busquedacedula').value = ''
}

function OcultarBuscadorRUC() {
    LimpiarControles()
    document.getElementById('Busquedacedula').style.display = 'none'
    document.getElementById('lbBuscador').style.display = 'none'
    document.getElementById('Busquedacedula').value = ''
}

export { AddEvents, GetProductos }