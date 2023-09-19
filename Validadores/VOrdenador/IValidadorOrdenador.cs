using TiendaOrdenadoresWebApi.Models;

namespace TiendaOrdenadoresWebApi.Validadores.VOrdenador
{
    public interface IValidadorOrdenador
    {
        bool IsValid(Ordenador ordenador);
    }
}
