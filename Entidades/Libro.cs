namespace Web_Api_Autores.Entidades
{
    public class Libro
    {

        public Libro() { 
        }

        public int Id { get; set; }
        
        public string Titulo { get; set; }

        public int AutorId { get; set; }

        public Autor Autor { get; set; }
    }
}
