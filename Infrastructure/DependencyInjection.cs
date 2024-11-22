using Data.Entities;
using Infrastructure.Repositoties;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped<Repository<Alumno>, AlumnoRepository>();
            services.AddScoped<Repository<Profesor>, ProfesorRepository>();
            services.AddScoped<Repository<Grado>, GradoRepository>();
            services.AddScoped<Repository<AlumnoGrado>, AlumnoGradoRepository>();
            services.AddScoped<IAlumnoGradoRepository, AlumnoGradoRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            return services;
        }
    }
}