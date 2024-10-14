using ESP8266.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ESP8266.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DispositivosController : ControllerBase
    {
        ProyectoContext dbContext = new ProyectoContext();

        [HttpGet]
        public IActionResult Get()
        {
            var dispositivos = dbContext.Dispositivos;

            return Ok(dispositivos);
        }

        [HttpGet("{id}")]
        public IActionResult BuscarDispositivo(int id)
        {
            var dispositivo = dbContext.Dispositivos.Where(p => p.IdDispositivo == id).FirstOrDefault();

            if (dispositivo != null)
            {
                return Ok(dispositivo);
            }
            else
            {
                return NotFound("No se encontró el dispositivo");
            }
        }

        [HttpPut("{id}")]
        public IActionResult ActualizarEstado(int id, [FromBody] bool estado)
        {
            var dispositivo = dbContext.Dispositivos.Find(id);

            if (dispositivo != null)
            {
                try
                {
                    dispositivo.Estado = estado;

                    dbContext.SaveChanges();

                    return Ok(dispositivo);
                }
                catch (Exception ex)
                {
                    return StatusCode(500, ex.Message);
                }
            }
            else
            {
                return NotFound("No se encontró el dispositivo");
            }
        }

        [HttpPost]
        public IActionResult InsertarDispositivo(Dispositivo nuevoDispositivo)
        {
            if (nuevoDispositivo != null)
            {
                if (!string.IsNullOrEmpty(nuevoDispositivo.Descripcion.Trim()))
                {
                    try
                    {
                        dbContext.Dispositivos.Add(nuevoDispositivo);

                        dbContext.SaveChanges();

                        return Ok(nuevoDispositivo);
                    }
                    catch (Exception ex)
                    {
                        return StatusCode(500, ex.Message);
                    }
                }
                else
                {
                    return BadRequest("Debe indicar una descripción para el dispositivo");
                }
            }
            else
            {
                return BadRequest("Debe enviar un objeto de tipo Dispositivo");
            }
        }

        [HttpDelete("{id}")]
        public IActionResult EliminarDispositivo(int id)
        {
            try
            {
                var dispositivo = dbContext.Dispositivos.Find(id);

                if (dispositivo != null)
                {
                    dbContext.Dispositivos.Remove(dispositivo);

                    dbContext.SaveChanges();

                    return Ok();
                }
                else
                {
                    return NotFound("No se encontró el dispositivo");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
