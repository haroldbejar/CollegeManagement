using Data.Context;
using Data.DTOs;
using Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositoties
{
    public class AlumnoRepository : Repository<Alumno>, IAlumnoRepostiroy
    {
        public AlumnoRepository(CollegeContext context) : base(context) { }

        public async Task<IReadOnlyCollection<AlumnoDTO>> GetAllAlumnos(int pageNumber, int pageSize)
        {
            var query = from a in _context.Alumnos
                        select new AlumnoDTO
                        {
                            Id = a.Id,
                            Nombre = a.Nombre,
                            Apellido = a.Apellido,
                            NombreCompleto = $"{a.Nombre} {a.Apellido}",
                            Genero = a.Genero,
                            FechaNacimiento = a.FechaNacimiento,
                        };
            return await query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize).ToListAsync();
        }
    }
}