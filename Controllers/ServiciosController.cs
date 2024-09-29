using Microsoft.AspNetCore.Mvc;
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

        // GET: api/<ServiciosController>
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                return Ok(_repository.GetAll());
            }
            catch (Exception)
            {

                return StatusCode(500, "error interno");
            }
        }

        // GET con filtros
        [HttpGet]
        public IActionResult Get(string nombre, string enPromocion)
        {
            try
            {
                return Ok(_repository.GetFilteredByNameAndPromotion(nombre, enPromocion));
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
                return Ok();
                //return Ok(_repository.(servicio));
            }
            catch (Exception)
            {

                return StatusCode(500, "error interno");
            }
        }

        // PUT api/<ServiciosController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] TServicio servicio)
        {
            try
            {
                return Ok(_repository.Update(servicio, id));
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
