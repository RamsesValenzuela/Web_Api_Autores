using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Web_Api_Autores;
using Web_Api_Autores.DTO;
using Web_Api_Autores.Entidades;

namespace Web_Api_Autores.Controllers
{
    [Route("api/libros/{libroId:int}/comentarios")]
    [ApiController]
    public class ComentariosController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper mapper;

        public ComentariosController(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            this.mapper = mapper;
        }

        // GET: api/Comentarios
        [HttpGet]
        public async Task<ActionResult<List<ComentarioDTO>>> Get(int libroId)
        {
            var existe = await _context.Libros.AnyAsync(libroDB => libroDB.Id == libroId);
            if (!existe)
            {
                return NotFound();
            }
            var comentarios = await _context.Comentarios.Where(comentarioDB => comentarioDB.Id == libroId).ToListAsync();
            return mapper.Map<List<ComentarioDTO>>(comentarios);
         }

        //// GET: api/Comentarios/5
        //[HttpGet("{id}")]
        //public async Task<ActionResult<Comentario>> GetComentario(int id)
        //{
        //    var comentario = await _context.Comentarios.FindAsync(id);

        //    if (comentario == null)
        //    {
        //        return NotFound();
        //    }

        //    return comentario;
        //}

        //// PUT: api/Comentarios/5
        //// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutComentario(int libroId, ComentarioCreacionDTO comentarioCreacionDTO)
        //{

        //    var existe = await _context.Libros.AnyAsync(libroDB => libroDB.Id == libroId);
        //    if (!existe)
        //    {
        //        return BadRequest();
        //    }

        //    _context.Entry(comentario).State = EntityState.Modified;

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!ComentarioExists(id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return NoContent();
        //}

        // POST: api/Comentarios
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult> Post(int libroId, ComentarioCreacionDTO comentarioCreacionDTO)
        {
            var existe = await _context.Libros.AnyAsync(libroDB => libroDB.Id == libroId);
            if (!existe)
            {
                return NotFound();
            }

            var comentario = mapper.Map<Comentario>(comentarioCreacionDTO);
            comentario.LibroId = libroId;
            _context.Add(comentario);
            await _context.SaveChangesAsync();
            return Ok();
        }

        //// DELETE: api/Comentarios/5
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteComentario(int id)
        //{
        //    var comentario = await _context.Comentarios.FindAsync(id);
        //    if (comentario == null)
        //    {
        //        return NotFound();
        //    }

        //    _context.Comentarios.Remove(comentario);
        //    await _context.SaveChangesAsync();

        //    return NoContent();
        //}

        //private bool ComentarioExists(int id)
        //{
        //    return _context.Comentarios.Any(e => e.Id == id);
        //}
    }
}
