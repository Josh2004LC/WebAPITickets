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
    public class UsuariosController : ControllerBase
    {
        private readonly ContextoBD _contexto;

        public UsuariosController(ContextoBD contexto)
        {
            _contexto = contexto;
        }

        // GET: api/Usuarios
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Usuarios>>> GetUsuarios()
        {
            return await _contexto.Usuarios.ToListAsync();
        }

        // GET: api/Usuarios/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Usuarios>> GetUsuario(int id)
        {
            var usuario = await _contexto.Usuarios.FindAsync(id);
            if (usuario == null)
            {
                return NotFound();
            }
            return usuario;
        }

        // GET: api/Usuarios/Rol/{rolId}
        [HttpGet("Rol/{rolId}")]
        public async Task<ActionResult<IEnumerable<Usuarios>>> GetUsuariosPorRol(int rolId)
        {
            var usuarios = await _contexto.Usuarios
                .Where(u => u.us_ro_identificador == rolId)
                .ToListAsync();
            if (usuarios == null || !usuarios.Any())
            {
                return NotFound();
            }
            return usuarios;
        }

        // GET: api/Usuarios/Estado/{estado}
        [HttpGet("Estado/{estado}")]
        public async Task<ActionResult<IEnumerable<Usuarios>>> GetUsuariosPorEstado(string estado)
        {
            var usuarios = await _contexto.Usuarios
                .Where(u => u.us_estado == estado)
                .ToListAsync();
            if (usuarios == null || !usuarios.Any())
            {
                return NotFound();
            }
            return usuarios;
        }

        // GET: api/Usuarios/validarIngreso?correo=...&clave=...
        [HttpGet("validarIngreso")]
        public async Task<ActionResult<Usuarios>> ValidarIngreso([FromQuery] string correo, [FromQuery] string clave)
        {
            var usuario = await _contexto.Usuarios
                .FirstOrDefaultAsync(u => u.us_correo == correo && u.us_clave == clave);

            if (usuario == null)
            {
                return Unauthorized("Credenciales inválidas");
            }

            if (usuario.us_estado != "A")
            {
                return Unauthorized("Usuario inactivo");
            }

            // Evitar exponer la contraseña en la respuesta
            usuario.us_clave = null;
            return usuario;
        }

        // POST: api/Usuarios
        [HttpPost]
        public async Task<ActionResult<Usuarios>> PostUsuario(Usuarios usuario)
        {
            usuario.us_fecha_adicion = DateTime.Now;
            _contexto.Usuarios.Add(usuario);
            await _contexto.SaveChangesAsync();
            return CreatedAtAction("GetUsuario", new { id = usuario.us_identificador }, usuario);
        }

        // PUT: api/Usuarios/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUsuario(int id, Usuarios usuario)
        {
            if (id != usuario.us_identificador)
            {
                return BadRequest();
            }

            var usuarioExistente = await _contexto.Usuarios.FindAsync(id);
            if (usuarioExistente == null)
            {
                return NotFound();
            }

            usuarioExistente.us_nombre_completo = usuario.us_nombre_completo;
            usuarioExistente.us_correo = usuario.us_correo;
            if (!string.IsNullOrEmpty(usuario.us_clave))
            {
                usuarioExistente.us_clave = usuario.us_clave;
            }
            usuarioExistente.us_ro_identificador = usuario.us_ro_identificador;
            usuarioExistente.us_estado = usuario.us_estado;
            usuarioExistente.us_fecha_modificacion = DateTime.Now;
            usuarioExistente.us_modificado_por = usuario.us_modificado_por;

            try
            {
                await _contexto.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UsuarioExists(id))
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

        // PATCH: api/Usuarios/{id}/CambiarEstado
        [HttpPatch("{id}/CambiarEstado")]
        public async Task<IActionResult> CambiarEstadoUsuario(int id, [FromBody] string nuevoEstado)
        {
            var usuario = await _contexto.Usuarios.FindAsync(id);
            if (usuario == null)
            {
                return NotFound();
            }
            usuario.us_estado = nuevoEstado;
            usuario.us_fecha_modificacion = DateTime.Now;
            await _contexto.SaveChangesAsync();
            return NoContent();
        }

        // PATCH: api/Usuarios/{id}/CambiarRol
        [HttpPatch("{id}/CambiarRol")]
        public async Task<IActionResult> CambiarRolUsuario(int id, [FromBody] int nuevoRolId)
        {
            var usuario = await _contexto.Usuarios.FindAsync(id);
            if (usuario == null)
            {
                return NotFound();
            }
            if (!await _contexto.Roles.AnyAsync(r => r.ro_identificador == nuevoRolId))
            {
                return BadRequest("El rol especificado no existe");
            }
            usuario.us_ro_identificador = nuevoRolId;
            usuario.us_fecha_modificacion = DateTime.Now;
            await _contexto.SaveChangesAsync();
            return NoContent();
        }

        // PATCH: api/Usuarios/{id}/CambiarClave
        [HttpPatch("{id}/CambiarClave")]
        public async Task<IActionResult> CambiarClaveUsuario(int id, [FromBody] string nuevaClave)
        {
            var usuario = await _contexto.Usuarios.FindAsync(id);
            if (usuario == null)
            {
                return NotFound();
            }
            usuario.us_clave = nuevaClave;
            usuario.us_fecha_modificacion = DateTime.Now;
            await _contexto.SaveChangesAsync();
            return NoContent();
        }

        // DELETE: api/Usuarios/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUsuario(int id)
        {
            var usuario = await _contexto.Usuarios.FindAsync(id);
            if (usuario == null)
            {
                return NotFound();
            }
            _contexto.Usuarios.Remove(usuario);
            await _contexto.SaveChangesAsync();
            return NoContent();
        }

        private bool UsuarioExists(int id)
        {
            return _contexto.Usuarios.Any(e => e.us_identificador == id);
        }
    }
}


