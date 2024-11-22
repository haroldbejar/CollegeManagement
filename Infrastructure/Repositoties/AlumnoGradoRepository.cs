using Data.Context;
using Data.DTOs;
using Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositoties
{
    public class AlumnoGradoRepository : Repository<AlumnoGrado>, IAlumnoGradoRepository
    {
        public AlumnoGradoRepository(CollegeContext context) : base(context)
        { }

        public async Task<IReadOnlyCollection<AlumnoGradoListDTO>> GetAllAlumnosGrados(int pageNumber, int pageSize)
        {
            var query = from ag in _context.AlumnosGrados
                        join a in _context.Alumnos on ag.AlumnoId equals a.Id
                        join g in _context.Grados on ag.GradoId equals g.Id
                        select new AlumnoGradoListDTO
                        {
                            Id = ag.Id,
                            AlumnoId = a.Id,
                            GradoId = g.Id,
                            GradoNombre = g.Nombre,
                            AlumnoNombre = $"{a.Nombre} {a.Apellido}",
                            Grupo = ag.Grupo
                        };
            return await query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }
    }
}