using System.Data;
using System.Data.SqlClient;

namespace API_LACTEOS.Servicios
{
    public class ServiciosBD
    {
        private readonly IConfiguration _config;

        public ServiciosBD(IConfiguration config)
        {
            _config = config;
        }

        public DataTable ObtenerDatosUsuarios()
        {
            DataTable dt = new DataTable();

            string query = "SELECT * FROM ViewUsuarios";

            string conexion = _config.GetConnectionString("CadenaSQL");

            using (SqlConnection conn = new SqlConnection(conexion))
            {
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    conn.Open();

                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dt);
                    }
                }
            }

            return dt;
        }

        public DataTable ObtenerDatosProveedores()
        {
            DataTable dt = new DataTable();

            string query = "SELECT * FROM ViewProveedores";

            string conexion = _config.GetConnectionString("CadenaSQL");

            using (SqlConnection conn = new SqlConnection(conexion))
            {
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    conn.Open();

                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dt);
                    }
                }
            }

            return dt;
        }

        public DataTable ObtenerDatosProductos()
        {
            DataTable dt = new DataTable();

            string query = "SELECT * FROM ViewProductos";

            string conexion = _config.GetConnectionString("CadenaSQL");

            using (SqlConnection conn = new SqlConnection(conexion))
            {
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    conn.Open();

                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dt);
                    }
                }
            }

            return dt;
        }

        public DataTable ObtenerDatosClientes()
        {
            DataTable dt = new DataTable();

            string query = "SELECT * FROM ViewClientes";

            string conexion = _config.GetConnectionString("CadenaSQL");

            using (SqlConnection conn = new SqlConnection(conexion))
            {
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    conn.Open();

                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dt);
                    }
                }
            }

            return dt;
        }

        public DataTable ObtenerFacturaPorNumero(string numeroFactura)
        {
            DataTable table = new DataTable();

            string query = @"SELECT * FROM ViewFacturaContado WHERE numeroFactura = @numeroFactura";

            string conexion = _config.GetConnectionString("CadenaSQL");

            using (SqlConnection connection = new SqlConnection(conexion))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@numeroFactura", numeroFactura);

                    connection.Open();
                    using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                    {
                        adapter.Fill(table);
                    }
                }
            }

            return table;
        }

        public DataTable ObtenerFacturaCreditoPorNumero(string numeroFactura)
        {
            DataTable table = new DataTable();

            string query = @"SELECT * FROM FacturaCredito WHERE numeroFactura = @numeroFactura";

            string conexion = _config.GetConnectionString("CadenaSQL");

            using (SqlConnection connection = new SqlConnection(conexion))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@numeroFactura", numeroFactura);

                    connection.Open();
                    using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                    {
                        adapter.Fill(table);
                    }
                }
            }

            return table;
        }

        public DataTable ObtenerFacturaCreditoPorFecha()
        {
            DataTable table = new DataTable();

            DateTime fechaActual = DateTime.Now;
            string fecha = fechaActual.ToString("yyyy-MM-dd");

            string query = @"SELECT * FROM FacturaCredito WHERE CONVERT(DATE, fechaCompra) = @fecha";

            string conexion = _config.GetConnectionString("CadenaSQL");

            using (SqlConnection connection = new SqlConnection(conexion))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@fecha", fecha);

                    connection.Open();
                    using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                    {
                        adapter.Fill(table);
                    }
                }
            }

            return table;
        }

        public DataTable ObtenerFacturaPorFechas()
        {
            DataTable table = new DataTable();

            DateTime fechaActual = DateTime.Now;
            string fecha = fechaActual.ToString("yyyy-MM-dd");

            string query = @"SELECT * FROM ViewFacturaContado WHERE CONVERT(DATE, fechaVenta) = @fecha";

            string conexion = _config.GetConnectionString("CadenaSQL");

            using (SqlConnection connection = new SqlConnection(conexion))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@fecha", fecha);

                    connection.Open();
                    using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                    {
                        adapter.Fill(table);
                    }
                }
            }

            return table;
        }

        public DataTable ObtenerFacturaArqueo()
        {
            DataTable table = new DataTable();

            DateTime fechaActual = DateTime.Now;
            string fecha = fechaActual.ToString("yyyy-MM-dd");

            string query = @"SELECT numeroFactura, nombreCliente, fechaVenta, totalVenta FROM ViewFacturaContado WHERE CONVERT(DATE, fechaVenta) = @fecha GROUP BY numeroFactura, nombreCliente, fechaVenta, totalVenta";

            string conexion = _config.GetConnectionString("CadenaSQL");

            using (SqlConnection connection = new SqlConnection(conexion))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@fecha", fecha);

                    connection.Open();
                    using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                    {
                        adapter.Fill(table);
                    }
                }
            }

            return table;
        }
    }
}
