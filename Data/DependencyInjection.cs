using Data.Context;
using Data.Profiles;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Data
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddData(this IServiceCollection services, string conString)
        {
            services.AddDbContext<CollegeContext>(options =>
            {
                options.UseSqlServer(conString);
            });

            return services;
        }

        public static IServiceCollection AddAutoMapperConfig(this IServiceCollection services)
        {
            services.AddAutoMapper(
                typeof(AlumnoProfile),
                typeof(ProfesorProfile),
                typeof(GradoProfile),
                typeof(AlumnoGradoProfile));

            return services;
        }
    }
}