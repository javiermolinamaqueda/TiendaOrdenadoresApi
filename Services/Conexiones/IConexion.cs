using Microsoft.Data.SqlClient;

namespace TiendaOrdenadoresWebApi.Services.Conexiones
{
    public interface IConexion
    {
        void CloseConnection();
        SqlCommand Ejecutar(string query);
        void StartConnection();
    }
}