using Aula06.Domain.Entities;
using AutoMapper;

namespace Aula06.Mapping
{
    public class ModelsToEntitiesMapping : Profile
    {
        public ModelsToEntitiesMapping()
        {
            CreateMap<UsuarioResponse, Usuario>().ReverseMap();  
        }

    }
}
