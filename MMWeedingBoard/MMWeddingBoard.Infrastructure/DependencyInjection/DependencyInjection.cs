using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MMWeddingBoard.Infrastructure.Persistence;

namespace MMWeddingBoard.Infrastructure.DependencyInjection;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection");

        services.AddDbContext<WeddingDbContext>(options =>
            options.UseNpgsql(connectionString));
        Console.WriteLine(connectionString);
        

        return services;
    }
}