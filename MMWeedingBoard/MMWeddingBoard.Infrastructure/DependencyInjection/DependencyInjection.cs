using Microsoft.Extensions.DependencyInjection;
using MMWeddingBoard.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMWeddingBoard.Infrastructure.DependencyInjection
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            //Beispiel: services.AddScoped<IProjectRepository, ProjectRepository>();

            // Registriere den DbContextFactory, damit er in der gesamten Anwendung verwendet werden kann
            services.AddScoped<WeddingDbContextFactory>();

            return services;
        }
    }
}
