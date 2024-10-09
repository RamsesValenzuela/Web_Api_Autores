using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Web_Api_Autores.DTO;
using Web_Api_Autores.Entidades;

namespace Web_Api_Autores.Controllers
{
    [ApiController]
    [Route("api/libros")]
    public class LibrosController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        public LibrosController(ApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<LibroDTOconAutores>> Get(int id)
        {
            var libro =  await context.Libros
                .Include(LibroDB => LibroDB.AutoresLibros)
                .ThenInclude(autorLibroDB => autorLibroDB.Autor)
                .FirstOrDefaultAsync(x => x.Id == id);

            libro.AutoresLibros = libro.AutoresLibros.OrderBy(x => x.Orden).ToList();

            return mapper.Map<LibroDTOconAutores>(libro);
        }

        [HttpPost]
        public async Task<ActionResult> Post(LibroCreacionDTO libroCreacionDTO)
        {

            if (libroCreacionDTO.AutoresIds == null) { return BadRequest("No se puede crear un libro sin autores"); }


            var autoresIds = await context.Autores.Where(autorDB => libroCreacionDTO.AutoresIds.Contains(autorDB.Id)).Select(x => x.Id).ToListAsync();

            if (libroCreacionDTO.AutoresIds.Count != autoresIds.Count)
            {
                return BadRequest("No existe uno de los autores Enviados");
            }

            var libro = mapper.Map<Libro>(libroCreacionDTO);

            if (libro.AutoresLibros != null) 
            {
                for(int i = 0; i < libro.AutoresLibros.Count; i++)
                {
                    libro.AutoresLibros[i].Orden = i;
                }
            }

            context.Add(libro);
            await context.SaveChangesAsync();
            return Ok();
        }
    }   
}
