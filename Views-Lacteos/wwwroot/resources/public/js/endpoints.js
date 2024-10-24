// USUARIOS
var GET_Login = 'http://localhost:5037/api/Usuario/Obtener/'

// PROVEEDORES
var POST_Proveedor = 'http://localhost:5037/api/Proveedores/Guardar/'
var PUT_Proveedor = 'http://localhost:5037/api/Proveedores/Editar/'
var DELETE_Proveedor = 'http://localhost:5037/api/Proveedores/Eliminar/'
var GET_Proveedores = 'http://localhost:5037/api/Proveedores/Lista'
var GET_Proveedor = 'http://localhost:5037/api/Proveedores/Obtener/'

// CLIENTES
var GET_Clientes = 'http://localhost:5037/api/Clientes/Lista'
var GET_Cliente = 'http://localhost:5037/api/Clientes/Obtener/'
var POST_Cliente = 'http://localhost:5037/api/Clientes/Guardar/'
var PUT_Cliente = 'http://localhost:5037/api/Clientes/Editar/'
var DELETE_Cliente = 'http://localhost:5037/api/Clientes/Eliminar/'

// PRODUCTOS

export { GET_Login, POST_Proveedor, PUT_Proveedor, DELETE_Proveedor, GET_Proveedores,
        GET_Proveedor, GET_Clientes, GET_Cliente, POST_Cliente, PUT_Cliente,
        DELETE_Cliente }