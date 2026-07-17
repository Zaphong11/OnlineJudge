using Microsoft.Extensions.DependencyInjection;

namespace ToDoAppApi.Domain;
//DI settings about Entity
public static class DependencyInjection
{
    public static IServiceCollection AddDomainDI(this IServiceCollection services)
    {
        return services;    
    }
}