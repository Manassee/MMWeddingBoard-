using Microsoft.EntityFrameworkCore;
using MMWeddingBoard.Infrastructure.Data;
using MMWeddingBoard.Infrastructure.DependencyInjection;
using MMWeddingBoard.Infrastructure.Persistence;
using System;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

try
{
    Console.WriteLine("\t\tPRÜFUNG: Alle Komponente der Infrastruktur werden hinzugefügt...\n\n");

    builder.Services.AddInfrastructure(builder.Configuration);
    Console.WriteLine("\t\tERFOLG: Alle Komponenten der Infrastruktur wurden erfolgreich hinzugefügt.\n\n");
}
catch (Exception ex)
{
    Console.WriteLine($"Fehler beim Hinzufügen der Infrastruktur: {ex.Message}");
    throw; 
}



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

if (app.Environment.IsDevelopment())
{
    using var scope = app.Services.CreateScope();
    var db = scope.ServiceProvider.GetRequiredService<WeddingDbContext>();
    await db.Database.MigrateAsync();
    await MMWeddingBoard.Infrastructure.Data.DataSeeder.SeedAsync(db);

}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
