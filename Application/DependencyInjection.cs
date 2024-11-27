using Data.DTOs;
using Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddScoped<IService<AlumnoDTO>, AlumnoService>();
            services.AddScoped<IService<ProfesorDTO>, ProfesorService>();
            services.AddScoped<IService<GradoDTO>, GradoService>();
            services.AddScoped<IService<AlumnoGradoDTO>, AlumnoGradoService>();
            services.AddScoped<IAlumnoGradoService, AlumnoGradoService>();
            services.AddScoped<IAcountService, AcountService>();
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IAlumnoService, AlumnoService>();
            return services;
        }
    }
}