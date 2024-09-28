using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Web_Api_Autores.Entidades;
using Web_Api_Autores.Servicios;

namespace Web_Api_Autores.Controllers
{

    [ApiController]
    [Route("api/[controller]")] //Cuando se pone entre corchetes se toma el nombre de controlador como nomrbe de la ruta
    public class AutoresController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IServicio servicio;

        public AutoresController(ApplicationDbContext context, IServicio servicio)
        {
            this.context = context;
            this.servicio = servicio;
        }

        [HttpGet]
        [HttpGet("listado")] //api/autores/listado
        [HttpGet("/listado")] //Se sobreEscribe la ruta api/autores y pasa a ser solo /listado sin necesidad de rutear la api/autores
        public async Task<List<Autor>> Get()
        {
            servicio.RealizarTarea();
            return  await context.Autores.Include(x => x.Libros).ToListAsync ();
        }

        [HttpGet("primero")] //Con solo poner entre los parentesis agregas un parametro mas al URL donde cambia api/autores/primero
        public async Task<ActionResult<Autor>> PrimerAutor([FromHeader] int miValor, [FromQuery] string nombre)
        {
            return await context.Autores.FirstOrDefaultAsync();
        }


        [HttpGet("{id:int}")]//Se entra a la ruta mediante una variable que tiene en la url 
        public async Task<ActionResult<Autor>> Get(int id)
        {

            var autor = await context.Autores.FirstOrDefaultAsync(x => x.Id == id);

            if (autor == null) { 
                return NotFound();
            }

            return autor;

        }

        [HttpGet("{nombre}")]//Se entra a la ruta mediante una variable que tiene en la url 
        public async Task<ActionResult<Autor>> Get(string nombre)
        {
            var autor = await context.Autores.FirstOrDefaultAsync(x => x.Nombre.Contains(nombre));

            if (autor == null)
            {
                return NotFound();
            }

            return autor;

        }


        [HttpPost]
        public async Task<ActionResult> Post(Autor autor)
        {
            var existAutor = await context.Autores.AnyAsync(x => x.Nombre == autor.Nombre);

            if (existAutor)
            {
                return BadRequest($"Ya existe un autor con el nombre {autor.Nombre}");
            }

            context.Add(autor);
            await context.SaveChangesAsync();
            return Ok();
        }

        [HttpPut("{id:int}")] //Mediante el ruteo de los parametros se actualiza mediante el parametro id
         public async Task<ActionResult> Put(Autor autor, int id)
        {
            if(autor.Id != id)
            {
                return BadRequest("El id del autort no coincide con el id de la URL");
            }

            var exist = await context.Autores.AnyAsync(x => x.Id == id);

            if (!exist)
            {
                return NotFound("El id del autor no coicide con el id del URL");
            }


            context.Update(autor);
            await context.SaveChangesAsync();
            return Ok();

        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var exist = await context.Autores.AnyAsync(x => x.Id == id);

            if (!exist)
            {
                return NotFound();
            }

            context.Remove(new Autor() { Id = id });

            await context.SaveChangesAsync();

            return Ok();
        }
    }
}
