using AutoMapper;
using RuletaAPi.DTOs;
using RuletaAPi.Models;

namespace RuletaAPi.Data
{
    public class AutoMapperProfiles :Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<Ruleta, RuletaDTO>().ReverseMap();
            CreateMap<Apuesta, ApuestaDTO>().ReverseMap();
        }
    }
}
