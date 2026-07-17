using ToDoAppApi.Application;
using ToDoAppApi.Infrastructure;

namespace ToDoAppApi.API;
//DI settings about API layer
public static class DependencyInjection
{
    public static IServiceCollection AddAppDI(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddApplicationDI()
            .AddInfrastructureDI(configuration);
        
        return services;    
    }
}