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
    public class RolesController : ControllerBase
    {
        private readonly ContextoBD _contexto;

        public RolesController(ContextoBD contexto)
        {
            _contexto = contexto;
        }

        // GET: api/Roles
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Roles>>> GetRoles()
        {
            return await _contexto.Roles.ToListAsync();
        }

        // GET: api/Roles/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Roles>> GetRol(int id)
        {
            var rol = await _contexto.Roles.FindAsync(id);

            if (rol == null)
            {
                return NotFound();
            }

            return rol;
        }

        // POST: api/Roles
        [HttpPost]
        public async Task<ActionResult<Roles>> PostRol(Roles rol)
        {
            
            rol.ro_fecha_adicion = DateTime.Now;

            _contexto.Roles.Add(rol);
            await _contexto.SaveChangesAsync();

            return CreatedAtAction("GetRol", new { id = rol.ro_identificador }, rol);
        }

        // PUT: api/Roles/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRol(int id, Roles rol)
        {
            if (id != rol.ro_identificador)
            {
                return BadRequest();
            }

            
            var rolExistente = await _contexto.Roles.FindAsync(id);

            if (rolExistente == null)
            {
                return NotFound();
            }

          
            rolExistente.ro_decripcion = rol.ro_decripcion;
            rolExistente.ro_fecha_modificacion = DateTime.Now;
            rolExistente.ro_modificado_por = rol.ro_modificado_por;

            try
            {
                await _contexto.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RolExists(id))
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

        // DELETE: api/Roles/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRol(int id)
        {
            var rol = await _contexto.Roles.FindAsync(id);
            if (rol == null)
            {
                return NotFound();
            }

            _contexto.Roles.Remove(rol);
            await _contexto.SaveChangesAsync();

            return NoContent();
        }

        private bool RolExists(int id)
        {
            return _contexto.Roles.Any(e => e.ro_identificador == id);
        }
    }
}