using AutoMapper;
using Data.DTOs;
using Data.Entities;

namespace Data.Profiles
{
    public class GradoProfile : Profile
    {
        public GradoProfile()
        {
            CreateMap<Grado, GradoDTO>();
            CreateMap<GradoDTO, Grado>();

        }
    }
}