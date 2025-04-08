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
    public class UrgenciasController : ControllerBase
    {
        private readonly ContextoBD _contexto;

        public UrgenciasController(ContextoBD contexto)
        {
            _contexto = contexto;
        }

        // GET: api/Urgencias
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Urgencias>>> GetUrgencias()
        {
            return await _contexto.Urgencias.ToListAsync();
        }

        // GET: api/Urgencias/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Urgencias>> GetUrgencia(int id)
        {
            var urgencia = await _contexto.Urgencias.FindAsync(id);
            if (urgencia == null)
            {
                return NotFound();
            }
            return urgencia;
        }

        // POST: api/Urgencias
        [HttpPost]
        public async Task<ActionResult<Urgencias>> PostUrgencia(Urgencias urgencia)
        {
            urgencia.ur_fecha_adicion = DateTime.Now;
            _contexto.Urgencias.Add(urgencia);
            await _contexto.SaveChangesAsync();
            return CreatedAtAction("GetUrgencia", new { id = urgencia.ur_identificador }, urgencia);
        }

        // PUT: api/Urgencias/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUrgencia(int id, Urgencias urgencia)
        {
            if (id != urgencia.ur_identificador)
            {
                return BadRequest();
            }

            var urgenciaExistente = await _contexto.Urgencias.FindAsync(id);
            if (urgenciaExistente == null)
            {
                return NotFound();
            }

            urgenciaExistente.ur_descripcion = urgencia.ur_descripcion;
            urgenciaExistente.ur_fecha_modificacion = DateTime.Now;
            urgenciaExistente.ur_modificado_por = urgencia.ur_modificado_por;

            await _contexto.SaveChangesAsync();
            return NoContent();
        }

        // DELETE: api/Urgencias/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUrgencia(int id)
        {
            var urgencia = await _contexto.Urgencias.FindAsync(id);
            if (urgencia == null)
            {
                return NotFound();
            }
            _contexto.Urgencias.Remove(urgencia);
            await _contexto.SaveChangesAsync();
            return NoContent();
        }
    }
}
