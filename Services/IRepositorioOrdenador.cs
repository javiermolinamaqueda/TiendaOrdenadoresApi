using TiendaOrdenadoresWebApi.Models;

namespace TiendaOrdenadoresWebApi.Services
{
    public interface IRepositorioOrdenador
    {
        List<Ordenador> GetAll();
        List<Ordenador>? GetByNull();
        void Add(Ordenador ordenador);
        void Delete(int Id);
        Ordenador? Find(int Id);
        void UpdatePedidoId(int id, int? pedidoId);

        void Update(int Id, Ordenador ordenador);
    }
}
