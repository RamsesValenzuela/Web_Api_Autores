using Microsoft.AspNetCore.Mvc;
using Web_Api_Autores.Entidades;

namespace Web_Api_Autores.Controllers
{

    [ApiController]
    [Route("api/autores")]
    public class AutoresController : ControllerBase
    {
        public AutoresController()
        {
        }

        [HttpGet]
        public ActionResult<List<Autor>> Get()
        {
            return new List<Autor>() { 
                new Autor() {Id =  1, Nombre = "Jose"},
                new Autor() {Id = 2, Nombre = "Gustavo" }
            };
        }
        
    }
}
