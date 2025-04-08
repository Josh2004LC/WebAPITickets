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
    public class ImportanciasController : ControllerBase
    {
        private readonly ContextoBD _contexto;

        public ImportanciasController(ContextoBD contexto)
        {
            _contexto = contexto;
        }

        // GET: api/Importancias
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Importancias>>> GetImportancias()
        {
            return await _contexto.Importancias.ToListAsync();
        }

        // GET: api/Importancias/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Importancias>> GetImportancia(int id)
        {
            var importancia = await _contexto.Importancias.FindAsync(id);
            if (importancia == null)
            {
                return NotFound();
            }
            return importancia;
        }

        // POST: api/Importancias
        [HttpPost]
        public async Task<ActionResult<Importancias>> PostImportancia(Importancias importancia)
        {
            importancia.im_fecha_adicion = DateTime.Now;
            _contexto.Importancias.Add(importancia);
            await _contexto.SaveChangesAsync();
            return CreatedAtAction("GetImportancia", new { id = importancia.im_identificador }, importancia);
        }

        // PUT: api/Importancias/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> PutImportancia(int id, Importancias importancia)
        {
            if (id != importancia.im_identificador)
            {
                return BadRequest();
            }

            var importanciaExistente = await _contexto.Importancias.FindAsync(id);
            if (importanciaExistente == null)
            {
                return NotFound();
            }

            importanciaExistente.im_descripcion = importancia.im_descripcion;
            importanciaExistente.im_fecha_modificacion = DateTime.Now;
            importanciaExistente.im_modificado_por = importancia.im_modificado_por;

            await _contexto.SaveChangesAsync();
            return NoContent();
        }

        // DELETE: api/Importancias/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteImportancia(int id)
        {
            var importancia = await _contexto.Importancias.FindAsync(id);
            if (importancia == null)
            {
                return NotFound();
            }
            _contexto.Importancias.Remove(importancia);
            await _contexto.SaveChangesAsync();
            return NoContent();
        }
    }
}
