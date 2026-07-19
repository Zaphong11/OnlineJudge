using HpuniJudgeApi.Application.Interfaces.Repositories;
using HpuniJudgeApi.Infrastructure.Data;
using HpuniJudgeApi.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace HpuniJudgeApi.Infrastructure;
//DI Settings about DataBase connection
public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureDI(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<AppDbContext>(options =>
        {
            options.UseNpgsql(configuration.GetConnectionString("DefaultConnection"));
        });
        services.AddScoped<IUserRepository, UserRepository>();
        
        return services;    
    }
}