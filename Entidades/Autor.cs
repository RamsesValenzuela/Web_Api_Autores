namespace Web_Api_Autores.Entidades
{
    public class Autor
    {

        public int Id { get; set; }
        public string Nombre { get; set; }


        public List<Libro> Libros { get; set; }
    }
}
