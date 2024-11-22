using AutoMapper;
using Data.DTOs;
using Data.Entities;

namespace Data.Profiles
{
    public class ProfesorProfile : Profile
    {
        public ProfesorProfile()
        {
            CreateMap<Profesor, ProfesorDTO>();
            CreateMap<ProfesorDTO, Profesor>();
        }
    }
}