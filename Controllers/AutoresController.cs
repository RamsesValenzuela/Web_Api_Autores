using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Web_Api_Autores.DTO;
using Web_Api_Autores.Entidades;


namespace Web_Api_Autores.Controllers
{

    [ApiController]
    [Route("api/[controller]")] //Cuando se pone entre corchetes se toma el nombre de controlador como nomrbe de la ruta
    public class AutoresController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        public AutoresController(ApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }


        [HttpGet]
        public async Task<List<AutorDTO>> Get()
        {
            var autores = await context.Autores.ToListAsync ();

            return mapper.Map<List<AutorDTO>>(autores);
        }



        [HttpGet("{id:int}", Name = "obtenerAutor")]//Se entra a la ruta mediante una variable que tiene en la url 
        public async Task<ActionResult<AutorDTOConLibro>> Get(int id)
        {

            var autor = await context.Autores
                .Include(autorDB => autorDB.AutoresLibros )
                .ThenInclude(autorLibroDB => autorLibroDB.Libro)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (autor == null)
            {
                return NotFound();
            }

            return mapper.Map<AutorDTOConLibro>(autor);
        }

        [HttpGet("{nombre}")]//Se entra a la ruta mediante una variable que tiene en la url 
        public async Task<ActionResult<List<AutorDTO>>> Get([FromRoute]string nombre)
        {
            var autores = await context.Autores.Where(x => x.Nombre.Contains(nombre)).ToListAsync();

            return mapper.Map<List<AutorDTO>>(autores);
        }


        [HttpPost]
        public async Task<ActionResult> Post([FromBody] AutorCreacionDTO autorCreacionDTO)
        {
            var existAutor = await context.Autores.AnyAsync(x => x.Nombre == autorCreacionDTO.Nombre);

            if (existAutor)
            {
                return BadRequest($"Ya existe un autor con el nombre {autorCreacionDTO.Nombre}");
            }

            var autor = mapper.Map<Autor>(autorCreacionDTO);


            context.Add(autor);
            await context.SaveChangesAsync();

            var autorDto = mapper.Map<AutorDTO>(autor);

            return CreatedAtRoute("obtenerAutor", new {autor.Id}, autorDto);
        }

        [HttpPut("{id:int}")] //Mediante el ruteo de los parametros se actualiza mediante el parametro id
         public async Task<ActionResult> Put(AutorCreacionDTO autorCreacionDto, int id)
        {

            var exist = await context.Autores.AnyAsync(x => x.Id == id);

            if (!exist)
            {
                return NotFound("El id del autor no coicide con el id del URL");
            }

            var autor = mapper.Map<Autor>(autorCreacionDto);

            autor.Id = id; 



            context.Update(autor);
            await context.SaveChangesAsync();
            return NoContent();

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
