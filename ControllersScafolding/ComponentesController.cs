using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TiendaOrdenadoresWebApi.Data;
using TiendaOrdenadoresWebApi.Models;
using TiendaOrdenadoresWebApi.Services;

namespace TiendaOrdenadoresWebApi.ControllersScafolding
{
    [Route("api/[controller]")]
    [ApiController]
    public class ComponentesController : ControllerBase
    {
        private readonly IRepositorioComponente _repositorioComponente;

        public ComponentesController(IRepositorioComponente repositorioComponente)
        {
            _repositorioComponente = repositorioComponente;
        }

        // GET: api/Componentes
        [HttpGet("GetAll")]
        public IEnumerable<Componente> GetComponentes()
        {
            return _repositorioComponente.GetAll().ToArray();
        }

        // GET: api/Componentes/5
        [HttpGet("GetComponente{id}")]
        public Componente GetComponente(int id)
        {
            var componente = _repositorioComponente.Find(id);

            return componente;
        }

        // PUT: api/Componentes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public void PutComponente(int id, Componente componente)
        {
            _repositorioComponente.Update(id, componente);
        }

        // POST: api/Componentes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost ("Create")]
        public void PostComponente(Componente componente)
        {
            _repositorioComponente.Add(componente);

            //return CreatedAtAction("GetComponente", new { id = componente.Id }, componente);
        }

        //DELETE: api/Componentes/5
        [HttpDelete("{id}")]
        public void DeleteComponente(int id)
        {
            _repositorioComponente.Delete(id);
        }

        [HttpGet("UpdateOrdenadorId{id}/{ordenadorId}")]
        public void UpdateOrdenadorId(int Id, int? OrdenadorId = null)
        {
            _repositorioComponente.UpdateOrdenadorId(Id, OrdenadorId);
        }

        [HttpGet("UpdateOrdenadorId{id}")]
        public void UpdateOrdenadorId(int Id)
        {
            _repositorioComponente.UpdateOrdenadorId(Id, null);
        }

        [HttpGet("GetByNull")]
        public IEnumerable<Componente> GetByNull()
        {
            return _repositorioComponente.GetByNull();
        }

        //[HttpGet("EditOrdenadorId{Id}")]
        //public void EditOrdenadorId(int Id)
        //{
        //    _repositorioComponente.UpdateOrdenadorId(Id, null);
        //}

        //private bool ComponenteExists(int id)
        //{
        //    return (_context.Componentes?.Any(e => e.Id == id)).GetValueOrDefault();
        //}
    }
}
