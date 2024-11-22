using Data.Context;
using Data.Entities;

namespace Infrastructure.Repositoties
{
    public class GradoRepository : Repository<Grado>
    {
        public GradoRepository(CollegeContext context) : base(context)
        { }
    }
}