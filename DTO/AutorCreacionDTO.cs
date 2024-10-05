using System.ComponentModel.DataAnnotations;
using Web_Api_Autores.Validaciones;

namespace Web_Api_Autores.DTO
{
    public class AutorCreacionDTO
    {
        [Required(ErrorMessage = "El campo {0} es requerido")]
        [PrimeraLetraMayuscula]
        public string Nombre { get; set; }


    }
}
