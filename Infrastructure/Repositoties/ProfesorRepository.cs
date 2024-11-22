using Data.Context;
using Data.Entities;

namespace Infrastructure.Repositoties
{
    public class ProfesorRepository : Repository<Profesor>
    {
        public ProfesorRepository(CollegeContext context) : base(context)
        { }
    }
}