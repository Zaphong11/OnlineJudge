using HpuniJudgeApi.Application;
using HpuniJudgeApi.Infrastructure;

namespace HpuniJudgeApi.API;
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