using System.Linq.Expressions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using PeluqueriaDLL.Data.Models;
using PeluqueriaDLL.Repositories;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PeluqueriaWebApiEF.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServiciosController : ControllerBase
    {
        private IServicioRepository _repository;

        public ServiciosController(IServicioRepository repository)
        {
            _repository = repository;
        }

        // GET con filtros
        [HttpGet]
        public IActionResult Get([FromQuery]string? nombre = null, [FromQuery]string? enPromocion = null)
        {
            try
            { 
                List<Expression<Func<TServicio,bool>>> filters = new List<Expression<Func<TServicio,bool>>>();
                if(nombre != null) filters.Add(servicio => servicio.Nombre.ToUpper() == nombre.ToUpper());
                if (enPromocion != null) filters.Add(servicio => servicio.EnPromocion.ToUpper() == enPromocion.ToUpper());
                return Ok(_repository.GetAllFilteredBy(filters));

            }
            catch (Exception)
            {

                return StatusCode(500, "error interno");
            }
        }

        // POST api/<ServiciosController>
        [HttpPost]
        public IActionResult Post([FromBody] TServicio servicio)
        {
            try
            {
                if (validarDatos(servicio))
                {
                    return Ok(_repository.Save(servicio));
                }

                else
                {
                    return BadRequest("Datos incorrectos o incompletos.");
                }
            }
            catch (Exception)
            {

                return StatusCode(500, "error interno");
            }
        }

        private bool validarDatos(TServicio servicio)
        {
            if (string.IsNullOrEmpty(servicio.Nombre))
            {
                return false; 
            }
            if (!decimal.TryParse(servicio.Costo.ToString(), out decimal costo) || costo < 0)
            {
                return false; 
            }
            if (servicio.EnPromocion.ToUpper() != "S" && servicio.EnPromocion.ToUpper() != "N")
            {
                return false; 
            }
            if (servicio.EstaActivo.ToUpper() != "S" && servicio.EstaActivo.ToUpper() != "N")
            {
                return false;
            }

            return true;
        }

        // PUT api/<ServiciosController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] TServicio servicio)
        {
            try
            {
                if (validarDatos(servicio))
                {
                    return Ok(_repository.Update(servicio, id));
                }
                else
                {
                    return BadRequest("Datos incorrectos o incompletos.");
                }
            }
            catch (Exception)
            {

                return StatusCode(500, "error interno");
            }
        }

        // DELETE api/<ServiciosController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                return Ok(_repository.Delete(id));
            }
            catch (Exception)
            {

                return StatusCode(500, "error interno");
            }
        }
    }
}
