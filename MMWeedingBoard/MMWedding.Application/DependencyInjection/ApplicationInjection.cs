using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MMWedding.Application.Abstractions;
using MMWedding.Application.Services;

namespace MMWedding.Application.DependencyInjection
{
    public static class ApplicationInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration)
        {
            // Hier können Sie Ihre Anwendungsdienste registrieren
            // Beispiel: services.AddScoped<IMyService, MyService>();
            services.AddScoped<IWeddingService, WeddingService>();

            return services;
        }
    }
}
