﻿using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Web_Api_Autores.Entidades;

namespace Web_Api_Autores.Controllers
{

    [ApiController]
    [Route("api/autores")]
    public class AutoresController : ControllerBase
    {
        private readonly ApplicationDbContext context;

        public AutoresController(ApplicationDbContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Autor>>> Get()
        {
            return await context.Autores.Include(x => x.Libros).ToListAsync();
        }

        [HttpPost]
        public async Task<ActionResult> Post(Autor autor)
        {
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
