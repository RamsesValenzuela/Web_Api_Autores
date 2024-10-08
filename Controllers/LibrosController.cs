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
        public async Task<ActionResult<LibroDTO>> Get(int id)
        {
            var libro =  await context.Libros.Include(LibroDB => LibroDB.Comentarios).FirstOrDefaultAsync(x => x.Id == id);

            return mapper.Map<LibroDTO>(libro);
        }

        [HttpPost]
        public async Task<ActionResult> Post(LibroCreacionDTO libroCreacionDTO)
        {

            var libro = mapper.Map<Libro>(libroCreacionDTO);

            context.Add(libro);
            await context.SaveChangesAsync();
            return Ok();
        }
    }   
}
