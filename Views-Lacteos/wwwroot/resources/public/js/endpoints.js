const host = 'http://localhost:5037/api' 

// USUARIOS
var GET_Login = `${host}/Usuario/Obtener/`
var GET_Usuarios = `${host}/Usuario/Lista/`
var POST_Usuario = `${host}/Usuario/Guardar/`
var PUT_Usuario = `${host}/Usuario/Editar/`
var DELETE_Usuario = `${host}/Usuario/Eliminar/`

// PROVEEDORES
var POST_Proveedor = `${host}/Proveedores/Guardar/`
var PUT_Proveedor = `${host}/Proveedores/Editar/`
var DELETE_Proveedor = `${host}/Proveedores/Eliminar/`
var GET_Proveedores = `${host}/Proveedores/Lista`
var GET_Proveedor = `${host}/Proveedores/Obtener/`

// CLIENTES
var GET_Clientes = `${host}/Clientes/Lista/`
var GET_Cliente = `${host}/Clientes/Obtener/`
var POST_Cliente = `${host}/Clientes/Guardar/`
var PUT_Cliente = `${host}/Clientes/Editar/`
var DELETE_Cliente = `${host}/Clientes/Eliminar/`

// PRODUCTOS
var GET_Productos = `${host}/Productos/Lista`
var GET_Producto = `${host}/Productos/Obtener/`
var POST_Producto = `${host}/Productos/Guardar/`
var PUT_Producto = `${host}/Productos/Editar/`
var DELETE_Producto = `${host}/Productos/Eliminar/`

// FACTURACION
var POST_Factura = `${host}/Ventas/GuardarV1/`

export { GET_Login, GET_Usuarios, POST_Usuario, PUT_Usuario, DELETE_Usuario,
        POST_Proveedor, PUT_Proveedor, DELETE_Proveedor, GET_Proveedores,
        GET_Proveedor, GET_Clientes, GET_Cliente, POST_Cliente, PUT_Cliente,
        DELETE_Cliente, GET_Productos, GET_Producto, POST_Producto, PUT_Producto,
        DELETE_Producto, POST_Factura }