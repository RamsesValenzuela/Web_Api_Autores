using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Web_Api_Autores.Validaciones;

namespace Web_Api_Autores.Entidades
{
    public class Autor 
    {

        public int Id { get; set; }
        [Required(ErrorMessage = "El campo {0} es requerido")]
        [PrimeraLetraMayuscula]
        [MaxLength(100)]
        public string Nombre { get; set; }


        public List<AutoresLibros> AutoresLibros { get; set; }

    }
}
