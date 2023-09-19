using Microsoft.EntityFrameworkCore;
using TiendaOrdenadoresWebApi.Data;
using TiendaOrdenadoresWebApi.Models;
using TiendaOrdenadoresWebApi.Validadores.VOrdenador;

using System.ComponentModel.DataAnnotations;
using Microsoft.Data.SqlClient;
using System.Data;
using TiendaOrdenadoresWebApi.Services.Conexiones;
using Humanizer;
using System.ComponentModel;
using Microsoft.AspNetCore.Components;

namespace TiendaOrdenadoresWebApi.Services
{
    public class RepositorioOrdenador : IRepositorioOrdenador
    {
        private readonly TiendaContext _context;
        private readonly IValidadorOrdenador _validadorOrdenador;
        private readonly IConexion _conexion;
        public RepositorioOrdenador(IConexion conexion,TiendaContext context, IValidadorOrdenador validador)
        {
            _conexion = conexion;
            _context = context;
            _validadorOrdenador = validador;
        }

        public void Add(Ordenador ordenador)
        {
            if (_validadorOrdenador.IsValid(ordenador))
            {
                string sPedidoId = "";
                string vPedidoId = "";
                if (ordenador.PedidoId!= null)
                {
                    this.SumaPrecioPedido(ordenador);
                    sPedidoId = $",'{ordenador.PedidoId}'";
                    vPedidoId = ", PedidoId";
                }
                SqlCommand command =  _conexion.Ejecutar
                     ($"Insert Into Ordenadores (Name,Precio" + vPedidoId + ") Values" +
               $"('{ordenador.Name}',"+0+ sPedidoId + ")");
                command.ExecuteNonQuery();
                _conexion.CloseConnection();
            }
        }


        public List<Ordenador> GetAll()
        {
            SqlCommand command = _conexion.Ejecutar(
                "SELECT Ordenadores.Id, Ordenadores.Name, Ordenadores.PedidoId, Ordenadores.Precio, " +
                "Componentes.Descripcion, Componentes.Id AS IdComponente " +
                "FROM Ordenadores LEFT OUTER JOIN Componentes ON Ordenadores.Id = Componentes.OrdenadorId");
            List<Ordenador> result = new List<Ordenador>();
            using (SqlDataReader dataReader = command.ExecuteReader())
            {
                // Loop over the results
                while (dataReader.Read())
                {
                    int Id = Convert.ToInt32(dataReader["Id"]);
                    var ordenadorBuscado = result.Find(x => x.Id == Id);
                    
                    if (ordenadorBuscado == null)
                    {
                        Ordenador ordenador = new Ordenador();
                        ordenador.Id = Convert.ToInt32(dataReader["Id"]);
                        ordenador.Name = Convert.ToString(dataReader["Name"]) ?? "";
                        ordenador.Precio = Convert.ToInt32(dataReader["Precio"]);
                        ordenador.PedidoId = dataReader["PedidoId"] != DBNull.Value ?
                            Convert.ToInt32(dataReader["PedidoId"]) : null;
                        if((dataReader["IdComponente"])!=DBNull.Value)
                        {
                            Componente componente = new Componente();
                            componente.Id = Convert.ToInt32(dataReader["IdComponente"]);
                            componente.Descripcion = Convert.ToString(dataReader["Descripcion"]) ?? "";
                            ordenador.Componentes.Add(componente);
                        }
                        result.Add(ordenador);
                    }
                    else
                    {
                        Componente componente = new Componente();
                        componente.Id = Convert.ToInt32(dataReader["IdComponente"]);
                        componente.Descripcion = Convert.ToString(dataReader["Descripcion"]) ?? "";
                        ordenadorBuscado.Componentes.Add(componente);
                    }
                }
            }
            _conexion.CloseConnection();
            return result;
        }

        public List<Ordenador> GetByNull()
        {
            SqlCommand command = _conexion.Ejecutar("Select * from Ordenadores where PedidoId is null");
            List<Ordenador> result = new List<Ordenador>();
            using (SqlDataReader dataReader = command.ExecuteReader())
            {
                // Loop over the results
                while (dataReader.Read())
                {
                    Ordenador ordenador = new Ordenador();
                    ordenador.Id = Convert.ToInt32(dataReader["Id"]);
                    ordenador.Name = Convert.ToString(dataReader["Name"]) ?? "";
                    ordenador.Precio = Convert.ToInt32(dataReader["Precio"]);
                    result.Add(ordenador);
                }
            }
            _conexion.CloseConnection();
            return result;
        }

        public void Delete(int Id)
        {
            var ordenador = this.Find(Id);
            if (ordenador.Id != 0)
            {
                SqlCommand command = _conexion.Ejecutar($"Delete From Ordenadores Where Id = '{Id}'");
                command.ExecuteNonQuery();
                _conexion.CloseConnection();
            }

        }

        public Ordenador Find(int Id)
        {
            SqlCommand command = _conexion.Ejecutar("Select *" +
                "From Ordenadores" +
                " Where Ordenadores.Id =" + Id);
            Ordenador ordenador = new Ordenador();
            using (SqlDataReader dataReader = command.ExecuteReader())
            {
                if (dataReader.Read())
                {
                    ordenador.Id = Convert.ToInt32(dataReader["Id"]);
                    ordenador.Name = Convert.ToString(dataReader["Name"]) ?? "";
                    ordenador.Precio = Convert.ToInt32(dataReader["Precio"]);
                }
            }
            _conexion.CloseConnection();
            return ordenador;

        }

        public void Update(int Id, Ordenador ordenador)
        {
            if (Id == ordenador.Id)
            {
                SqlCommand command = _conexion.Ejecutar($"Update Ordenadores SET Name = '{ordenador.Name}'" +
                    $" WHERE Id = {Id}");

                command.ExecuteNonQuery();
                _conexion.CloseConnection();
            }
        }
        public void UpdatePedidoId(int Id, int? PedidoId)
        {
            var ordenador = this.Find(Id);
            if (ordenador != null)
            {
                if (ordenador.PedidoId == null)
                {
                    ordenador.PedidoId = PedidoId;
                    this.SumaPrecioPedido(ordenador);
                    this.Update(Id, ordenador);
                }
                else
                {
                    this.RestaPrecioPedido(ordenador);
                    ordenador.PedidoId = null;
                    this.Update(Id, ordenador);
                }


            }
        }
        public void SumaPrecioPedido(Ordenador ordenador)
        {
            SqlCommand command = _conexion.Ejecutar($"Update Pedidos SET Precio = Pedidos.Precio+'{ordenador.Precio}'" +
                $" WHERE Id = '{ordenador.PedidoId}'");
            command.ExecuteNonQuery();
            _conexion.CloseConnection();
        }

        public void RestaPrecioPedido(Ordenador ordenador)
        {
            SqlCommand command = _conexion.Ejecutar($"Update Pedidos SET Precio = Pedidos.Precio-'{ordenador.Precio}'" +
                $" WHERE Id = '{ordenador.PedidoId}'");
            command.ExecuteNonQuery();
            _conexion.CloseConnection();
        }
    }
}
