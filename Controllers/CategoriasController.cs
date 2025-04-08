using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPITickets.Database;
using WebAPITickets.Models;

namespace WebAPITickets.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriasController : ControllerBase
    {
        private readonly ContextoBD _contexto;

        public CategoriasController(ContextoBD contexto)
        {
            _contexto = contexto;
        }

        // GET: api/Categorias
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Categorias>>> GetCategorias()
        {
            return await _contexto.Categorias.ToListAsync();
        }

        // GET: api/Categorias/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Categorias>> GetCategoria(int id)
        {
            var categoria = await _contexto.Categorias.FindAsync(id);
            if (categoria == null)
            {
                return NotFound();
            }
            return categoria;
        }

        // POST: api/Categorias
        [HttpPost]
        public async Task<ActionResult<Categorias>> PostCategoria(Categorias categoria)
        {
            categoria.ca_fecha_adicion = DateTime.Now;
            _contexto.Categorias.Add(categoria);
            await _contexto.SaveChangesAsync();
            return CreatedAtAction("GetCategoria", new { id = categoria.ca_identificador }, categoria);
        }

        // PUT: api/Categorias/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCategoria(int id, Categorias categoria)
        {
            if (id != categoria.ca_identificador)
            {
                return BadRequest();
            }

            var categoriaExistente = await _contexto.Categorias.FindAsync(id);
            if (categoriaExistente == null)
            {
                return NotFound();
            }

            categoriaExistente.ca_descripcion = categoria.ca_descripcion;
            categoriaExistente.ca_fecha_modificacion = DateTime.Now;
            categoriaExistente.ca_modificado_por = categoria.ca_modificado_por;

            await _contexto.SaveChangesAsync();
            return NoContent();
        }

        // DELETE: api/Categorias/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategoria(int id)
        {
            var categoria = await _contexto.Categorias.FindAsync(id);
            if (categoria == null)
            {
                return NotFound();
            }
            _contexto.Categorias.Remove(categoria);
            await _contexto.SaveChangesAsync();
            return NoContent();
        }
    }
}
