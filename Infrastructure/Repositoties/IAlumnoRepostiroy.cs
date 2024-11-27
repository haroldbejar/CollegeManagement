using Data.DTOs;

namespace Infrastructure.Repositoties
{
    public interface IAlumnoRepostiroy
    {
        Task<IReadOnlyCollection<AlumnoDTO>> GetAllAlumnos(int pageNumber, int pageSize);
    }
}