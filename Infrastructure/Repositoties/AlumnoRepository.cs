using Data.Context;
using Data.Entities;

namespace Infrastructure.Repositoties
{
    public class AlumnoRepository : Repository<Alumno>
    {
        public AlumnoRepository(CollegeContext context) : base(context) { }

    }
}