using Microsoft.AspNetCore.Mvc;
using TiendaOrdenadoresWebApi.Models;
using TiendaOrdenadoresWebApi.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TiendaOrdenadoresWebApi.ControllersScafolding
{
    [Route("api/[controller]")]
    [ApiController]
    public class PedidosController : ControllerBase
    {
        private readonly IRepositorioPedido _repositorioPedido;
        public PedidosController(IRepositorioPedido repositorioPedido)
        {
            _repositorioPedido = repositorioPedido;
        }
 
        [HttpGet("GetAll")]
        public IEnumerable<Pedido> GetPedidos()
        {
            return _repositorioPedido.GetAll().ToArray();
        }

        [HttpGet("GetPedido{id}")]
        public Pedido? GetPedido(int id)
        {
            var pedido = _repositorioPedido.Find(id);

            return pedido;
        }

        [HttpPost("Create")]
        public void PostPedido(Pedido pedido)
        {
            _repositorioPedido.Add(pedido);

            //return CreatedAtAction("GetComponente", new { id = componente.Id }, componente);
        }

        [HttpDelete("{id}")]
        public void DeletePedido(int id)
        {
            _repositorioPedido.Delete(id);
        }


    }
}
