using AutoMapper;
using Data.DTOs;
using Data.Entities;

namespace Data.Profiles
{
    public class AlumnoProfile : Profile
    {
        public AlumnoProfile()
        {
            CreateMap<Alumno, AlumnoDTO>();
            CreateMap<AlumnoDTO, Alumno>();
            CreateMap<AlumnoCreateDTO, Alumno>();
            CreateMap<Alumno, AlumnoCreateDTO>();
            CreateMap<AlumnoDTO, AlumnoCreateDTO>();
            CreateMap<AlumnoCreateDTO, AlumnoDTO>();
        }
    }
}