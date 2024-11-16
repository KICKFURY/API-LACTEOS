// Inventario.js

class Inventario {
    constructor() {
        this.productos = [];
    }

    agregarProducto(nombre, cantidad, precio) {
        const producto = {
            id: this.productos.length + 1,
            nombre: nombre,
            cantidad: cantidad,
            precio: precio
        };
        this.productos.push(producto);
        console.log(`Producto agregado: ${nombre}`);
    }

    eliminarProducto(id) {
        const index = this.productos.findIndex(producto => producto.id === id);
        if (index !== -1) {
            const eliminado = this.productos.splice(index, 1);
            console.log(`Producto eliminado: ${eliminado[0].nombre}`);
        } else {
            console.log(`Producto con ID ${id} no encontrado.`);
        }
    }

    actualizarCantidad(id, cantidad) {
        const producto = this.productos.find(producto => producto.id === id);
        if (producto) {
            producto.cantidad = cantidad;
            console.log(`Cantidad actualizada para ${producto.nombre}: ${cantidad}`);
        } else {
            console.log(`Producto con ID ${id} no encontrado.`);
        }
    }

    obtenerInventario() {
        return this.productos;
    }

    mostrarInventario() {
        console.log("Inventario:");
        this.productos.forEach(producto => {
            console.log(`ID: ${producto.id}, Nombre: ${producto.nombre}, Cantidad: ${producto.cantidad}, Precio: $${producto.precio.toFixed(2)}`);
        });
    }
}

export default Inventario;
