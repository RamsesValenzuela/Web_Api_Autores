using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Web_Api_Autores.Validaciones;

namespace Web_Api_Autores.Entidades
{
    public class Autor : IValidatableObject
    {

        
        public int Id { get; set; }
        [Required(ErrorMessage = "El campo {0} es requerido")]
        [StringLength(maximumLength:5, ErrorMessage = "El campo {0} no debe de tener mas de {1} caracteres")]
        public string Nombre { get; set; }

        
        public List<Libro> Libros { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (!string.IsNullOrEmpty(Nombre))
            {
                var primeraLetra = Nombre[0].ToString();

                if(primeraLetra != primeraLetra.ToUpper())
                {
                    yield return new ValidationResult("La primera letra debe de ser mayuscula",
                        new string[] { nameof(Nombre) });
                }
            }

            

        }
    }
}
