using Data.DTOs;

namespace Application.Services
{
    public interface IAlumnoService
    {
        Task<IReadOnlyCollection<AlumnoDTO>> GetAllAlumnos(int pageNumber, int pageSize);
        Task AddAlumnoAsync(AlumnoCreateDTO alumnoDTO);
        AlumnoCreateDTO MapAlumnoDTO(AlumnoDTO alumnoDTO);
    }
}