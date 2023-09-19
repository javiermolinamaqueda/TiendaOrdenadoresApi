using TiendaOrdenadoresWebApi.Models;

namespace TiendaOrdenadoresWebApi.Services
{
    public interface IRepositorioPedido
    {
        List<Pedido> GetAll();
        //List<Ordenador> GetByNull();
        void Add(Pedido pedido);
        void Delete(int Id);
        Pedido? Find(int Id);
        //void UpdateFacturaId(int id, int pedidoId);

        //void Update(int Id, Ordenador ordenador);
    }
}
