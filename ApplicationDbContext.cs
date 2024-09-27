using Microsoft.EntityFrameworkCore;
using Web_Api_Autores.Entidades;

namespace Web_Api_Autores
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Autor> Autores { get; set; }
            
        public DbSet<Libro> Libros { get; set; }

    }

}
