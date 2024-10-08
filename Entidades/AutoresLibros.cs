namespace Web_Api_Autores.Entidades
{
    public class AutoresLibros
    {

        public int LibroId { get; set; }
        public int AutorId { get; set; }

        public int Orden { get; set; }

        public Libro Libro { get; set; }

        public Autor Autor { get; set; }


    }
}
