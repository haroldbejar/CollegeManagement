using AutoMapper;
using Data.DTOs;
using Data.Entities;

namespace Data.Profiles
{
    public class AlumnoGradoProfile : Profile
    {
        public AlumnoGradoProfile()
        {
            CreateMap<AlumnoGrado, AlumnoGradoDTO>();
            CreateMap<AlumnoGradoDTO, AlumnoGrado>();
            CreateMap<AlumnoGradoListDTO, AlumnoGrado>();
            CreateMap<AlumnoGrado, AlumnoGradoListDTO>();

        }
    }
}