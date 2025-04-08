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

        // GET: api/Tiquetes/Urgencia/{urgencia}
        [HttpGet("Urgencia/{urgencia}")]
        public async Task<ActionResult<IEnumerable<Tiquetes>>> GetTiquetesPorUrgencia(int urgencia)
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

        // GET: api/Tiquetes/Usuario/{idUsuario}
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

        // GET: api/Tiquetes/Estado/{estado}
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

        // DELETE: api/Tiquetes/{id}
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

        // ==================== Endpoints para Soporte ====================

        // GET: api/Tiquetes/soporte/{idUsuario}
        [HttpGet("soporte/{idUsuario}")]
        public async Task<ActionResult<IEnumerable<Tiquetes>>> GetTicketsSoporte(int idUsuario)
        {
            var tickets = await _contexto.Tiquetes
                .Where(t => t.ti_adicionado_por == idUsuario.ToString())
                .ToListAsync();
            if (tickets == null || !tickets.Any())
            {
                return NotFound();
            }
            return tickets;
        }

        // GET: api/Tiquetes/soporte/creados
        [HttpGet("soporte/creados")]
        public async Task<ActionResult<IEnumerable<Tiquetes>>> GetTicketsCreados()
        {
            var tickets = await _contexto.Tiquetes
                .Where(t => t.ti_estado == "Creado")
                .ToListAsync();
            if (tickets == null || !tickets.Any())
            {
                return NotFound();
            }
            return tickets;
        }

        // GET: api/Tiquetes/soporte/pendientes
        [HttpGet("soporte/pendientes")]
        public async Task<ActionResult<IEnumerable<Tiquetes>>> GetTicketsPendientesSoporte()
        {
            var tickets = await _contexto.Tiquetes
                .Where(t => t.ti_estado == "Pendiente")
                .ToListAsync();
            if (tickets == null || !tickets.Any())
            {
                return NotFound();
            }
            return tickets;
        }

        // GET: api/Tiquetes/soporte/creados/diarios
        [HttpGet("soporte/creados/diarios")]
        public async Task<ActionResult<IEnumerable<Tiquetes>>> GetTicketsCreadosDiarios()
        {
            var today = DateTime.Today;
            var tickets = await _contexto.Tiquetes
                .Where(t => t.ti_estado == "Creado" && t.ti_fecha_adicion.Date == today)
                .ToListAsync();
            if (tickets == null || !tickets.Any())
            {
                return NotFound();
            }
            return tickets;
        }

        // ==================== Endpoints para Analista ====================

        // GET: api/Tiquetes/analista/{idUsuario}
        [HttpGet("analista/{idUsuario}")]
        public async Task<ActionResult<IEnumerable<Tiquetes>>> GetTicketsAnalista(int idUsuario)
        {
            var tickets = await _contexto.Tiquetes
                .Where(t => t.ti_us_id_asigna == idUsuario)
                .ToListAsync();
            if (tickets == null || !tickets.Any())
            {
                return NotFound();
            }
            return tickets;
        }

        // GET: api/Tiquetes/analista/resueltos
        [HttpGet("analista/resueltos")]
        public async Task<ActionResult<IEnumerable<Tiquetes>>> GetTicketsResueltos()
        {
            var tickets = await _contexto.Tiquetes
                .Where(t => t.ti_estado == "Resuelto")
                .ToListAsync();
            if (tickets == null || !tickets.Any())
            {
                return NotFound();
            }
            return tickets;
        }

        // GET: api/Tiquetes/analista/pendientes
        [HttpGet("analista/pendientes")]
        public async Task<ActionResult<IEnumerable<Tiquetes>>> GetTicketsPendientesAnalista()
        {
            var tickets = await _contexto.Tiquetes
                .Where(t => t.ti_estado == "Pendiente")
                .ToListAsync();
            if (tickets == null || !tickets.Any())
            {
                return NotFound();
            }
            return tickets;
        }

        // PATCH: api/Tiquetes/analista/actualizarSolucionTicket/{id}
        [HttpPatch("analista/actualizarSolucionTicket/{id}")]
        public async Task<IActionResult> ActualizarSolucionTicket(int id, [FromBody] string solucion)
        {
            var ticket = await _contexto.Tiquetes.FindAsync(id);
            if (ticket == null)
            {
                return NotFound();
            }
            ticket.ti_solucion = solucion;
            ticket.ti_estado = "Resuelto";
            ticket.ti_fecha_modificacion = DateTime.Now;
            await _contexto.SaveChangesAsync();
            return NoContent();
        }

        // GET: api/Tiquetes/analista/resueltos/diario
        [HttpGet("analista/resueltos/diario")]
        public async Task<ActionResult<IEnumerable<Tiquetes>>> GetTicketsResueltosDiario()
        {
            var today = DateTime.Today;
            var tickets = await _contexto.Tiquetes
                .Where(t => t.ti_estado == "Resuelto" &&
                            t.ti_fecha_modificacion.HasValue && t.ti_fecha_modificacion.Value.Date == today)
                .ToListAsync();
            if (tickets == null || !tickets.Any())
            {
                return NotFound();
            }
            return tickets;
        }

        private bool TiqueteExists(int id)
        {
            return _contexto.Tiquetes.Any(e => e.ti_identificador == id);
        }
    }
}
