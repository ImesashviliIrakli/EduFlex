using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Persistance.Data;

namespace Persistance;

public static class PersistanceServiceRegistration
{
    public static IServiceCollection AddPersistanceServices(this IServiceCollection services, IConfiguration configuartion)
    {
        services.AddDbContext<AppDBContext>
        (
            options =>
            {
                options.UseSqlServer
                (
                    configuartion.GetConnectionString("DefaultConnection")
                );
            }
        );

        return services;
    }
}
