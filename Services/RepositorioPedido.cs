using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using TiendaOrdenadoresWebApi.Data;
using TiendaOrdenadoresWebApi.Models;
using TiendaOrdenadoresWebApi.Services.Conexiones;
using TiendaOrdenadoresWebApi.Validadores.VOrdenador;

namespace TiendaOrdenadoresWebApi.Services
{
    public class RepositorioPedido : IRepositorioPedido
    {
        
        private readonly IConexion _conexion;
        public RepositorioPedido(IConexion conexion)
        {
            _conexion = conexion;
        }

        public void Add(Pedido pedido)
        {
            SqlCommand command = _conexion.Ejecutar
                    ($"Insert Into Pedidos (Name,Precio) Values ('{pedido.Name}',"+0+")");
            command.ExecuteNonQuery();
            _conexion.CloseConnection();
        }


        public List<Pedido> GetAll()
        {
            SqlCommand command = _conexion.Ejecutar("Select * from Pedidos");
            List<Pedido> result = new List<Pedido>();
            using (SqlDataReader dataReader = command.ExecuteReader())
            {
                // Loop over the results
                while (dataReader.Read())
                {
                    Pedido pedido= new Pedido();
                    pedido.Id = Convert.ToInt32(dataReader["Id"]);
                    pedido.Name = Convert.ToString(dataReader["Name"]) ?? "";
                    pedido.Precio = Convert.ToInt32(dataReader["Precio"]);
                    //incluir ordenadores
                    result.Add(pedido);
                }
            }
            _conexion.CloseConnection();
            return result;
        }

        public void Delete(int Id)
        {
            var pedido = this.Find(Id);
            if (pedido.Id != 0)
            {
                SqlCommand command = _conexion.Ejecutar($"Delete From Pedidos Where Id = '{Id}'");
                command.ExecuteNonQuery();
                _conexion.CloseConnection();
            }

        }

        public Pedido Find(int Id)
        {
            SqlCommand command = _conexion.Ejecutar("Select *" +
                "From Pedidos" +
                " Where Pedidos.Id =" + Id);
            Pedido pedido = new Pedido();
            using (SqlDataReader dataReader = command.ExecuteReader())
            {
                if (dataReader.Read())
                {
                    pedido.Id = Convert.ToInt32(dataReader["Id"]);
                    pedido.Name = Convert.ToString(dataReader["Name"]) ?? "";
                    pedido.Precio = Convert.ToInt32(dataReader["Precio"]);
                    //incluir ordenadores y componentes
                }
            }
            _conexion.CloseConnection();
            return pedido;

        }
    }
}
