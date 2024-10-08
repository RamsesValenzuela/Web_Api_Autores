using System.ComponentModel.DataAnnotations;
using Web_Api_Autores.Validaciones;

namespace Web_Api_Autores.DTO
{
    public class LibroCreacionDTO
    {

        [PrimeraLetraMayuscula]
        [StringLength(maximumLength: 250)]
        public string Titulo { get; set; }

        public List<int> AutoresIds { get; set; }

    }
}
