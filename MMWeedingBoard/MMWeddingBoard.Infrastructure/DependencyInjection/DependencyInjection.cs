using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MMWedding.Application.Abstractions;
using MMWeddingBoard.Infrastructure.Persistence;
using MMWeddingBoard.Infrastructure.Repositories;

namespace MMWeddingBoard.Infrastructure.DependencyInjection;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddDbContext<WeddingDbContext>(options =>
             options.UseSqlite(
                 configuration.GetConnectionString("DefaultConnection")));

        services.AddScoped<IWeddingRepository, WeddingRepository>();

        return services;
    }
}