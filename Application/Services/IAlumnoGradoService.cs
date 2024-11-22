using Data.DTOs;

namespace Application.Services
{
    public interface IAlumnoGradoService
    {
        Task<IReadOnlyCollection<AlumnoGradoListDTO>> GetAllAlumnosGrados(int pageNumber, int pageSize);
    }
}