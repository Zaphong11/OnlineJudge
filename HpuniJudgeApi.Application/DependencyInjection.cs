using HpuniJudgeApi.Application.Interfaces;
using HpuniJudgeApi.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace HpuniJudgeApi.Application;
//DI settings about Business Logic
public static class DependencyInjection
{
    public static IServiceCollection AddApplicationDI(this IServiceCollection services)
    {
        services.AddScoped<IAuthService, AuthService>();
        return services;    
    }
}