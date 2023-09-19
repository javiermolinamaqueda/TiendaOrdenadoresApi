using TiendaOrdenadoresWebApi.Models;


namespace TiendaOrdenadoresWebApi.Services
{
    public class FakeRepositorioOrdenador : IRepositorioOrdenador
    {
        private readonly List<Ordenador> _ordenadorList =
            new()
            {
                new Ordenador() {Id = 1, Name = "Javier", PedidoId = 2},
                new Ordenador() {Id = 2, Name = "Maria"}
            };

        public void Add(Ordenador ordenador)
        {
            this._ordenadorList.Add(ordenador);
        }

        public void Delete(int Id)
        {
            var ordenador = _ordenadorList.Find(x => x.Id == Id);
            if (ordenador is not null)
            {
                this._ordenadorList.Remove(ordenador);
            }
        }

        public Ordenador? Find(int Id)
        {
            return _ordenadorList.First(x => x.Id == Id);
        }

        public List<Ordenador> GetAll()
        {
            return this._ordenadorList;
        }

        public List<Ordenador>? GetByNull()
        {
            return this.GetAll()?.Where(x => x.PedidoId == null).ToList();
        }

        public void Update(int Id, Ordenador ordenador)
        {
            var ordenadorAntiguo = _ordenadorList.Find(p => p.Id == Id);
            if (ordenadorAntiguo is not null && Id == ordenador.Id)
            {
                int index = _ordenadorList.IndexOf(ordenadorAntiguo);
                _ordenadorList[index] = ordenador;
            }

        }

        public void UpdatePedidoId(int id, int? pedidoId)
        {
            var ordenador = this.Find(id);

            if (ordenador != null)
            {
                ordenador.PedidoId = pedidoId;
                this.Update(id, ordenador);

            }

        }
    }
}
