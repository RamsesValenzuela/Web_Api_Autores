using System.ComponentModel.DataAnnotations;
using Web_Api_Autores.Validaciones;

namespace Web_Api_Autores.Entidades
{
    public class Libro
    {

     
        
        public int Id { get; set; }
        [Required]
        [PrimeraLetraMayuscula]
        [StringLength(maximumLength: 250)]
        public string Titulo { get; set; }

        public List<Comentario> Comentarios { get; set; }

        public List<AutoresLibros> AutoresLibros { get; set; }

    }
}
