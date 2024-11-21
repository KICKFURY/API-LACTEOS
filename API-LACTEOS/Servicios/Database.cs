using System.Data.SqlClient;
using System.Data.SqlTypes;

namespace API_LACTEOS.Servicios
{
    public class Database
    {
        private readonly IConfiguration config;

        public Database(IConfiguration _config)
        {
            this.config = _config;
        }

        public void CrearBackup(string backupPath)
        {
            string query = $"BACKUP DATABASE LACTEOS_BD TO DISK = '{backupPath}' WITH NOFORMAT, NOINIT, NAME = 'LacteosLaGranja-FullBackup', SKIP, NOREWIND, NOUNLOAD, STATS = 10";

            string conexion = config.GetConnectionString("CadenaSQL");

            using (SqlConnection connection = new SqlConnection(conexion))
            {
                SqlCommand command = new SqlCommand(query, connection);
                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
            }
        }

        public void RestaurarBackup(string backupPath)
        {
            string query = $"RESTORE DATABASE LACTEOS_BD TO DISK = '{backupPath}' WITH FILE = 1, NOUNLOAD, REPLACE, STATS = 5";

            string conexion = config.GetConnectionString("CadenaSQL");

            using (SqlConnection connection = new SqlConnection(conexion))
            {
                SqlCommand command = new SqlCommand(query, connection);
                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
            }
        }
    }
}
