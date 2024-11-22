using Data.DTOs;

namespace Infrastructure.Repositoties
{
    public interface IAlumnoGradoRepository
    {
        Task<IReadOnlyCollection<AlumnoGradoListDTO>> GetAllAlumnosGrados(int pageNumber, int pageSize);
    }
}