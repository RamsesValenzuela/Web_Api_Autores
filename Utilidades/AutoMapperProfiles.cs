using AutoMapper;
using Web_Api_Autores.DTO;
using Web_Api_Autores.Entidades;

namespace Web_Api_Autores.Utilidades
{
    public class AutoMapperProfiles : Profile
    {
         public AutoMapperProfiles()
        {
            CreateMap<AutorCreacionDTO, Autor>();
        }
    }
}
