using ESP8266.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ESP8266.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        ProyectoContext dbContext = new ProyectoContext();

        [HttpGet]
        public IActionResult Get([FromQuery] string? nombre = null, [FromQuery] string? clave = null)
        {
            if (!string.IsNullOrEmpty(nombre) || !string.IsNullOrEmpty(clave))
            {
                if (!string.IsNullOrEmpty(nombre) && !string.IsNullOrEmpty(clave))
                {
                    var usuario = dbContext.Usuarios.Where(p => p.Nombre == nombre & clave == p.Clave).FirstOrDefault();

                    if (usuario != null)
                    {
                        return Ok(usuario);
                    }
                    else
                    {
                        return NotFound("No se encontró el usuario");
                    }
                }
                else
                {
                    return BadRequest("Si filtra los datos debe indicar el nombre y clave");
                }
            }
            else
            {
                return Ok(dbContext.Usuarios);
            }
        }

        [HttpGet("{id}")]
        public IActionResult Buscar(int id)
        {
            var usuario = dbContext.Usuarios.Where(p => p.IdUsuario == id).FirstOrDefault();

            if (usuario != null)
            {
                return Ok(usuario);
            }

            return Ok();
        }

        [HttpPut("{id}")]
        public IActionResult ActualizarUsuario(int id, Usuario usuario)
        {
            if (usuario != null)
            {
                var _usuario = dbContext.Usuarios.Find(id);

                if (_usuario != null)
                {
                    if (!string.IsNullOrEmpty(_usuario.Nombre.Trim()) && !string.IsNullOrEmpty(_usuario.Clave.Trim()))
                    {
                        try
                        {
                            _usuario.Nombre = usuario.Nombre;
                            _usuario.Clave = usuario.Clave;

                            dbContext.SaveChanges();

                            return Ok(_usuario);
                        }
                        catch (Exception ex)
                        {
                            return StatusCode(500, ex.Message);
                        }
                    }
                    else
                    {
                        return BadRequest("Debe indicar un nombre y contraseña");
                    }

                }
                else
                {
                    return NotFound("No se encontró el usuario");
                }
            }
            else
            {
                return BadRequest("Debe enviar un objeto de tipo Usuario para poder actualizar");
            }
        }

        [HttpPost]
        public IActionResult InsertarDispositivo(Usuario nuevoUsuario)
        {
            if (nuevoUsuario != null)
            {
                if (!string.IsNullOrEmpty(nuevoUsuario.Nombre.Trim()) && !string.IsNullOrEmpty(nuevoUsuario.Clave.Trim()))
                {
                    try
                    {
                        dbContext.Usuarios.Add(nuevoUsuario);

                        dbContext.SaveChanges();

                        return Ok(nuevoUsuario);
                    }
                    catch (Exception ex)
                    {
                        return StatusCode(500, ex.Message);
                    }
                }
                else
                {
                    return BadRequest("Debe indicar un nombre y clave para el usuario");
                }
            }
            else
            {
                return BadRequest("Debe enviar un objeto de tipo Usuario");
            }
        }

        [HttpDelete("{id}")]
        public IActionResult EliminarUsuario(int id)
        {
            try
            {
                var usuario = dbContext.Usuarios.Find(id);

                if (usuario != null)
                {
                    dbContext.Usuarios.Remove(usuario);

                    dbContext.SaveChanges();

                    return Ok();
                }
                else
                {
                    return NotFound("No se encontró el usuario");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
