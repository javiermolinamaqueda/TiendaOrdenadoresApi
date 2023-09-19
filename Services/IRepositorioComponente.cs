using TiendaOrdenadoresWebApi.Models;

namespace TiendaOrdenadoresWebApi.Services
{
    public interface IRepositorioComponente
    {
        void Add(Componente componente);
        void Delete(int Id);
        IEnumerable<Componente> GetAll();
        List<Componente> GetByNull();
        Componente Find(int Id);
        //List<Componente> GetAllByOrdenadorId(int ordenadorId);

        void Update(int Id, Componente componente);
        void UpdateOrdenadorId(int id, int? OrdenadorId);
    }
}
