using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration.Json;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders; // Keine Änderung hier
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace MMWeddingBoard.Infrastructure.Persistence
{
    public class WeddingDbContextFactory : IDesignTimeDbContextFactory<WeddingDbContext>
    {
        public WeddingDbContext CreateDbContext(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: false)
                .Build();

            var connectionString = configuration.GetConnectionString("DefaultConnection");
            if (string.IsNullOrWhiteSpace(connectionString))
            {
                throw new InvalidOperationException("Connection string 'DefaultConnection' wurde nicht im appsettings.json gefunden.\n");
            }
            else
            {
                Console.WriteLine("Connection string wurde im appsettings.json gefunden.\n");
            }
                

            var optionsBuilder = new DbContextOptionsBuilder<WeddingDbContext>();
            optionsBuilder.UseSqlite(connectionString, b => b.MigrationsAssembly("MMWeddingBoard.Infrastructure")); // <- Für PostgreSQL


            return new WeddingDbContext(optionsBuilder.Options);
        }
    }
}
