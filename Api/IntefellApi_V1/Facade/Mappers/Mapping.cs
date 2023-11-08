using AutoMapper;
using Entities.Entities;
using Facade.DTOs;

namespace Facade.Mappers
{

    public static class Mapping
    {
        public static IMapper configMapper()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            });
            return config.CreateMapper();
        }
    }
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<PaisDTO,Pais>().ReverseMap();
            CreateMap<RegionDTO, Region>().ReverseMap();
            CreateMap<ComunaDTO, Comuna>().ReverseMap();
            CreateMap<UserDTO, User>().ReverseMap();
            CreateMap<LoggerDTO, Logger>().ReverseMap();
            CreateMap<AyudaDTO, Ayuda>().ReverseMap();
            CreateMap<PersonasDTO,Persona>().ReverseMap();
            CreateMap<AyudaPersonaDTO, PersonaAyuda>().ReverseMap();
            CreateMap<AyudaComunaDTO, AyudasComuna>().ReverseMap();
            CreateMap<AyudaRegionDTO,AyudasRegion>().ReverseMap();
        }

    }
}
