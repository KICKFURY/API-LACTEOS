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
    }
}
