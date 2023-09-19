using Microsoft.Data.SqlClient;

namespace TiendaOrdenadoresWebApi.Services.Conexiones
{
    public class Conexion : IConexion
    {
        readonly IConfiguration _configuration;
        readonly string _connectionString = "";
        readonly SqlConnection _connection;
        public Conexion(IConfiguration configuration)
        {
            _configuration = configuration;
            _connection = new SqlConnection(_configuration["ConnectionStrings:AppConnection"]);
        }

        public SqlCommand Ejecutar(string query)
        {
            SqlCommand command = new SqlCommand(query, _connection);
            _connection.Open();
            return command;

        }
        public void StartConnection()
        {
            _connection.Open();
        }
        public void CloseConnection()
        {
            _connection.Close();
        }
    }
}
