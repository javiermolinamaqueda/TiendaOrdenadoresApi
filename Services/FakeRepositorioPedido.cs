using TiendaOrdenadoresWebApi.Models;

namespace TiendaOrdenadoresWebApi.Services
{
    public class FakeRepositorioPedido : IRepositorioPedido
    {
        private readonly List<Pedido> pedidos;

        public FakeRepositorioPedido()
        {
            pedidos = new()
            {
                new Pedido { Id = 1 },
                new Pedido { Id = 2 }
            };
        }
        public void Add(Pedido pedido)
        {
            this.pedidos.Add(pedido);
        }

        public void Delete(int Id)
        {
            var pedido = this.pedidos.Find(x => x.Id == Id);
            if (pedido is not null)
            {
                this.pedidos.Remove(pedido);
            }

        }

        public Pedido? Find(int Id)
        {
            return this.pedidos.First(x => x.Id == Id);
        }

        public List<Pedido> GetAll()
        {
            return this.pedidos;
        }
    }
}
