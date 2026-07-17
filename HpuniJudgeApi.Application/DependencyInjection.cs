using Microsoft.Extensions.DependencyInjection;

namespace ToDoAppApi.Application;
//DI settings about Business Logic
public static class DependencyInjection
{
    public static IServiceCollection AddApplicationDI(this IServiceCollection services)
    {
        return services;    
    }
}