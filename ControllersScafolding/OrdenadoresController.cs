using Microsoft.AspNetCore.Mvc;
using TiendaOrdenadoresWebApi.Models;
using TiendaOrdenadoresWebApi.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TiendaOrdenadoresWebApi.ControllersScafolding
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdenadoresController : ControllerBase
    {
        private readonly IRepositorioOrdenador _repositorioOrdenador;

        public OrdenadoresController(IRepositorioOrdenador repositorioOrdenador)
        {
            _repositorioOrdenador = repositorioOrdenador;
        }

        // GET: api/Ordenadores
        [HttpGet("GetAll")]
        public IEnumerable<Ordenador> GetOrdenadores()
        {
            return _repositorioOrdenador.GetAll().ToArray();
        }

        // GET: api/Ordenadores/5
        [HttpGet("GetOrdenador{id}")]
        public Ordenador? GetOrdenador(int id)
        {
            var ordenador = _repositorioOrdenador.Find(id);

            return ordenador;
        }

        // PUT: api/Ordenadores/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public void PutOrdenador(int id, Ordenador ordenador)
        {
            _repositorioOrdenador.Update(id, ordenador);
        }

        // POST: api/Ordenadores
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("Create")]
        public void PostOrdenador(Ordenador ordenador)
        {
            _repositorioOrdenador.Add(ordenador);

            //return CreatedAtAction("GetComponente", new { id = componente.Id }, componente);
        }

        //DELETE: api/Ordenadores/5
        [HttpDelete("{id}")]
        public void DeleteOrdenador(int id)
        {
            _repositorioOrdenador.Delete(id);
        }

        [HttpGet("UpdatePedidoId{id}/{PedidoId}")]
        public void UpdatePedidoId(int Id, int? PedidoId = null)
        {
            _repositorioOrdenador.UpdatePedidoId(Id, PedidoId);
        }

        [HttpGet("GetByNull")]
        public IEnumerable<Ordenador>? GetByNull()
        {
            return _repositorioOrdenador.GetByNull();
        }
    }
}
