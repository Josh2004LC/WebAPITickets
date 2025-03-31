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
    public class TiquetesController : ControllerBase
    {
        private readonly ContextoBD _contexto;

        public TiquetesController(ContextoBD contexto)
        {
            _contexto = contexto;
        }

        // GET: api/Tiquetes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Tiquetes>>> GetTiquetes()
        {
            return await _contexto.Tiquetes.ToListAsync();
        }

        // GET: api/Tiquetes/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Tiquetes>> GetTiquete(int id)
        {
            var tiquete = await _contexto.Tiquetes.FindAsync(id);

            if (tiquete == null)
            {
                return NotFound();
            }

            return tiquete;
        }

        // GET: api/Tiquetes/Urgencia/Alta
        [HttpGet("Urgencia/{urgencia}")]
        public async Task<ActionResult<IEnumerable<Tiquetes>>> GetTiquetesPorUrgencia(string urgencia)
        {
            var tiquetes = await _contexto.Tiquetes
                .Where(t => t.ti_urgencia == urgencia)
                .ToListAsync();

            if (tiquetes == null || !tiquetes.Any())
            {
                return NotFound();
            }

            return tiquetes;
        }

        // GET: api/Tiquetes/Usuario/{id}
        [HttpGet("Usuario/{idUsuario}")]
        public async Task<ActionResult<IEnumerable<Tiquetes>>> GetTiquetesPorUsuario(int idUsuario)
        {
            var tiquetes = await _contexto.Tiquetes
                .Where(t => t.ti_us_id_asigna == idUsuario)
                .ToListAsync();

            if (tiquetes == null || !tiquetes.Any())
            {
                return NotFound();
            }

            return tiquetes;
        }

        // GET: api/Tiquetes/Estado/Abierto
        [HttpGet("Estado/{estado}")]
        public async Task<ActionResult<IEnumerable<Tiquetes>>> GetTiquetesPorEstado(string estado)
        {
            var tiquetes = await _contexto.Tiquetes
                .Where(t => t.ti_estado == estado)
                .ToListAsync();

            if (tiquetes == null || !tiquetes.Any())
            {
                return NotFound();
            }

            return tiquetes;
        }

        // POST: api/Tiquetes
        [HttpPost]
        public async Task<ActionResult<Tiquetes>> PostTiquete(Tiquetes tiquete)
        {
            
            tiquete.ti_fecha_adicion = DateTime.Now;

            _contexto.Tiquetes.Add(tiquete);
            await _contexto.SaveChangesAsync();

            return CreatedAtAction("GetTiquete", new { id = tiquete.ti_identificador }, tiquete);
        }

        // PUT: api/Tiquetes/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTiquete(int id, Tiquetes tiquete)
        {
            if (id != tiquete.ti_identificador)
            {
                return BadRequest();
            }

           
            var tiqueteExistente = await _contexto.Tiquetes.FindAsync(id);

            if (tiqueteExistente == null)
            {
                return NotFound();
            }

            
            tiqueteExistente.ti_asunto = tiquete.ti_asunto;
            tiqueteExistente.ti_categoria = tiquete.ti_categoria;
            tiqueteExistente.ti_us_id_asigna = tiquete.ti_us_id_asigna;
            tiqueteExistente.ti_urgencia = tiquete.ti_urgencia;
            tiqueteExistente.ti_importancia = tiquete.ti_importancia;
            tiqueteExistente.ti_estado = tiquete.ti_estado;
            tiqueteExistente.ti_solucion = tiquete.ti_solucion;
            tiqueteExistente.ti_fecha_modificacion = DateTime.Now;
            tiqueteExistente.ti_modificado_por = tiquete.ti_modificado_por;

            try
            {
                await _contexto.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TiqueteExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // PATCH: api/Tiquetes/{id}/CambiarEstado
        [HttpPatch("{id}/CambiarEstado")]
        public async Task<IActionResult> CambiarEstadoTiquete(int id, [FromBody] string nuevoEstado)
        {
            var tiquete = await _contexto.Tiquetes.FindAsync(id);

            if (tiquete == null)
            {
                return NotFound();
            }

            tiquete.ti_estado = nuevoEstado;
            tiquete.ti_fecha_modificacion = DateTime.Now;

            await _contexto.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/Tiquetes/
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTiquete(int id)
        {
            var tiquete = await _contexto.Tiquetes.FindAsync(id);
            if (tiquete == null)
            {
                return NotFound();
            }

            _contexto.Tiquetes.Remove(tiquete);
            await _contexto.SaveChangesAsync();

            return NoContent();
        }

        private bool TiqueteExists(int id)
        {
            return _contexto.Tiquetes.Any(e => e.ti_identificador == id);
        }
    }
}