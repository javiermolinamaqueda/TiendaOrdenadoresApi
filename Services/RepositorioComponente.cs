using Humanizer;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using System.Data;
using TiendaOrdenadoresWebApi.Data;
using TiendaOrdenadoresWebApi.Models;
using TiendaOrdenadoresWebApi.Services.Conexiones;
using TiendaOrdenadoresWebApi.Validadores.VComponente;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
//using TiendaOrdenadores;
namespace TiendaOrdenadoresWebApi.Services
{
    public class RepositorioComponente : IRepositorioComponente
    {
        readonly TiendaContext _context;
        readonly IValidadorComponente _validador;
        readonly IConexion _conexion;


        public RepositorioComponente(IConexion conexion, TiendaContext context, IValidadorComponente validador)
        {
            this._context = context;
            _validador = validador;
            _conexion = conexion;
        }
        public void Add (Componente componente)
        {
            if(_validador.IsValid(componente))
            {
                string sOrdenadorId = "";
                string vOrdenadorId = "";
                if(componente.OrdenadorId != null)
                {
                    this.SumaPrecioOrdenador(componente);
                    sOrdenadorId = $",'{componente.OrdenadorId}'";
                    vOrdenadorId = ", OrdenadorId";
                }
                 SqlCommand command =  _conexion.Ejecutar
                     ($"Insert Into Componentes (Serie, Descripcion, TipoComponente, Almacenamiento," +
                $"Cores, Calor, Precio"+vOrdenadorId+") Values" +
                $"('{componente.Serie}','{componente.Descripcion}', '{componente.TipoComponente}', '{componente.Almacenamiento}'," +
                $"'{componente.Cores}', '{componente.Calor}', '{componente.Precio}'"+sOrdenadorId+")");
                command.ExecuteNonQuery ();
                _conexion.CloseConnection();
            }
        }
        

        public IEnumerable<Componente> GetAll()
        {
            List<Componente> result = new List<Componente>();
            SqlCommand command = _conexion.Ejecutar("SELECT Componentes.Id, Componentes.TipoComponente, " +
                "Componentes.Descripcion, Componentes.Serie, Componentes.Precio, Componentes.Calor, " +
                "Componentes.Almacenamiento, Componentes.Cores, Componentes.OrdenadorId, Ordenadores.Id AS IdOrdenador, " +
                "Ordenadores.Name, Ordenadores.PedidoId, Ordenadores.Precio AS PrecioOrdenador " +
                "FROM Componentes LEFT OUTER JOIN Ordenadores ON Componentes.OrdenadorId = Ordenadores.Id");
            //SqlCommand command = _conexion.Ejecutar("Select * from Componentes");
            using (SqlDataReader dataReader = command.ExecuteReader())
            {
                // Loop over the results
                while (dataReader.Read())
                {
                    Componente componente = new Componente();
                    componente.Id = Convert.ToInt32(dataReader["Id"]);
                    componente.Serie = Convert.ToString(dataReader["Serie"]) ?? "";
                    componente.Descripcion = Convert.ToString(dataReader["Descripcion"]) ?? "";
                    componente.Precio = Convert.ToDouble(dataReader["Precio"]);
                    componente.TipoComponente = Convert.ToInt32(dataReader["TipoComponente"]);
                    componente.Almacenamiento = Convert.ToInt32(dataReader["Almacenamiento"]);
                    componente.Cores = Convert.ToInt32(dataReader["Cores"]);
                    componente.Calor = Convert.ToInt32(dataReader["Calor"]);
                    componente.OrdenadorId = dataReader["OrdenadorId"] != DBNull.Value ?
                        Convert.ToInt32(dataReader["OrdenadorId"]) : null;
                    if(componente.OrdenadorId  != null)
                    {
                        Ordenador ordenador = new Ordenador();
                        ordenador.Id = Convert.ToInt32(dataReader["IdOrdenador"]);
                        ordenador.Name = Convert.ToString(dataReader["Name"]) ?? "";
                        ordenador.Precio = Convert.ToInt32(dataReader["PrecioOrdenador"]);
                        componente.Ordenador = ordenador;
                        
                    }
                    result.Add(componente);

                }
            }
            _conexion.CloseConnection();
            return result;
        }

        public List<Componente> GetByNull()
        {
            SqlCommand command = _conexion.Ejecutar("Select * from Componentes where OrdenadorId is null");
            List<Componente> result = new List<Componente>();
            using (SqlDataReader dataReader = command.ExecuteReader())
            {
                // Loop over the results
                while (dataReader.Read())
                {
                    Componente componente = new Componente();
                    componente.Id = Convert.ToInt32(dataReader["Id"]);
                    componente.Serie = Convert.ToString(dataReader["Serie"]) ?? "";
                    componente.Descripcion = Convert.ToString(dataReader["Descripcion"]) ?? "";
                    componente.Precio = Convert.ToDouble(dataReader["Precio"]);
                    componente.TipoComponente = Convert.ToInt32(dataReader["TipoComponente"]);
                    componente.Almacenamiento = Convert.ToInt32(dataReader["Almacenamiento"]);
                    componente.Cores = Convert.ToInt32(dataReader["Cores"]);
                    componente.Calor = Convert.ToInt32(dataReader["Calor"]);
                    result.Add(componente);
                }
            }
            _conexion.CloseConnection();
            return result;
        }

        public void Delete (int Id)
        {
            var componente = this.Find(Id);
            if(componente.Id !=0)
            {
                SqlCommand command = _conexion.Ejecutar($"Delete From Componentes Where Id = '{Id}'");
                command.ExecuteNonQuery();
                _conexion.CloseConnection();
            }
         
        }

        public Componente Find(int Id)
        {
            SqlCommand command = _conexion.Ejecutar("Select *" +
                "From Componentes" +
                " Where Componentes.Id ="+Id);
            Componente componente = new Componente();
            using (SqlDataReader dataReader = command.ExecuteReader())
            {
                if (dataReader.Read())
                {
                    componente.Id = Convert.ToInt32(dataReader["Id"]);
                    componente.Serie = Convert.ToString(dataReader["Serie"]) ?? "";
                    componente.Descripcion = Convert.ToString(dataReader["Descripcion"]) ?? "";
                    componente.Precio = Convert.ToDouble(dataReader["Precio"]);
                    componente.TipoComponente = Convert.ToInt32(dataReader["TipoComponente"]);
                    componente.Almacenamiento = Convert.ToInt32(dataReader["Almacenamiento"]);
                    componente.Cores = Convert.ToInt32(dataReader["Cores"]);
                    componente.Calor = Convert.ToInt32(dataReader["Calor"]);
                    componente.OrdenadorId = dataReader["OrdenadorId"] != DBNull.Value ?
                        Convert.ToInt32(dataReader["OrdenadorId"]) : null;
                }
            }
            _conexion.CloseConnection();
            return componente;
               
        }

        public void Update(int Id, Componente componente)
        {
            if(Id == componente.Id && _validador.IsValid(componente))
            {
                SqlCommand command = _conexion.Ejecutar("UpdateComponentes");
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@Id", componente.Id);
                command.Parameters.AddWithValue("@Serie", componente.Serie);
                command.Parameters.AddWithValue("@TipoComponente", componente.TipoComponente);
                command.Parameters.AddWithValue("@Descripcion", componente.Descripcion);
                command.Parameters.AddWithValue("@Almacenamiento", componente.Almacenamiento);
                command.Parameters.AddWithValue("@Cores", componente.Cores);
                command.Parameters.AddWithValue("@Precio", componente.Precio);
                command.Parameters.AddWithValue("@Calor", componente.Calor);
                command.Parameters.AddWithValue("@OrdenadorId", componente.OrdenadorId);

                command.ExecuteNonQuery();
                _conexion.CloseConnection();
            }
        }
        public void UpdateOrdenadorId(int Id, int? OrdenadorId)
        {
            var componente = this.Find(Id);
            if (componente != null)
            {
                if(componente.OrdenadorId == null)
                {
                    componente.OrdenadorId = OrdenadorId;
                    this.SumaPrecioOrdenador(componente);
                    this.Update(Id, componente);
                }
                else
                {
                    this.RestaPrecioOrdenador(componente);
                    componente.OrdenadorId = null;
                    this.Update(Id, componente);
                }
                

            }
        }

        //

        public void SumaPrecioOrdenador(Componente componente)
        {
            SqlCommand command = _conexion.Ejecutar($"Update Ordenadores SET Precio = Precio+'{componente.Precio}'" +
                $" WHERE Id = '{componente.OrdenadorId}'");
            command.ExecuteNonQuery();
            _conexion.CloseConnection();
        }

        public void RestaPrecioOrdenador(Componente componente)
        {
            SqlCommand command = _conexion.Ejecutar($"Update Ordenadores SET Precio = Precio-" +
                $"'{componente.Precio}'" +
          $" WHERE Id = '{componente.OrdenadorId}'");
            command.ExecuteNonQuery();
            _conexion.CloseConnection();
        }
    }

}
