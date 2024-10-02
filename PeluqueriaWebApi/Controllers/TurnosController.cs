using System.Globalization;
using System.Linq.Expressions;
using Microsoft.AspNetCore.Mvc;
using PeluqueriaDLL.Data.Models;
using PeluqueriaDLL.Repositories;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PeluqueriaWebApiEF.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TurnosController : ControllerBase
    {
        private ITurnoRepository _repository;

        public TurnosController(ITurnoRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public IActionResult GetFilteredBy([FromQuery]string? cliente, [FromQuery] string? fecha, [FromQuery]string? estaActivo  )
        {
            var filtros = new List<Expression<Func<TTurno, bool>>>();
            if (cliente != null)
            {
                filtros.Add(turno => turno.Cliente.ToUpper() == cliente.ToUpper());
            }
            if(fecha != null)
            {
                filtros.Add(turno => turno.Fecha == fecha);
            }
            if (estaActivo != null)
            {
                filtros.Add(turno => turno.EstaActivo.ToUpper() == estaActivo.ToUpper());
            }

            try
            {
                return Ok(_repository.GetFilteredBy(filtros));
            }
            catch (Exception)
            {

                return StatusCode(500, "Error interno.");
            }
        }

        // POST api/<TurnosController>
        [HttpPost]
        public IActionResult Post([FromBody] TTurno turno)
        {
            try
            {
                if (ValidarDatos(turno))
                {
                    return Ok(_repository.Create(turno));
                }
                else
                {
                    return BadRequest("Datos incorrectos o incompletos");
                }
            }
            catch (Exception)
            {

                return StatusCode(500, "Error interno.");
            }
        }

        private bool ValidarDatos(TTurno turno)
        {
            DateTime fechaRecibida = Convert.ToDateTime(turno.Fecha);
            DateTime fechaMinima = fechaRecibida.AddDays(1);
            DateTime fechaMaxima = fechaRecibida.AddDays(45);

            if (string.IsNullOrEmpty(turno.Fecha))
            {
                turno.Fecha = DateTime.Today.AddDays(1).ToString("dd/MM/yyyy");
            }
            else {            
                if(fechaRecibida<fechaMinima || fechaRecibida > fechaMaxima)
                {
                    return false;
                }
            }
            if(string.IsNullOrEmpty(turno.Hora) || string.IsNullOrEmpty(turno.Cliente))
            {
                return false;
            }
            if(turno.EstaActivo.ToUpper() == "N".ToUpper())
            {
                return false; // para validar que se creen estando activos
            }
            if(turno.TDetallesTurnos.Count < 1)
            {
                return false; // para validar que se cree por lo menos con un detalle?
            }
            return true;
        }

        // PUT api/<TurnosController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] TTurno turno)
        {
            try
            {
                return Ok(_repository.Update(id,turno));
            }
            catch (Exception)
            {

                return StatusCode(500, "Error interno.");
            }
        }

        // DELETE api/<TurnosController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                return Ok(_repository.Delete(id));
            }
            catch (Exception)
            {

                return StatusCode(500, "Error interno.");
            }
        }
    }
}
