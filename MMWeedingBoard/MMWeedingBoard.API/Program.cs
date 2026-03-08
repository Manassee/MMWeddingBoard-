using Microsoft.EntityFrameworkCore;
using MMWeddingBoard.Infrastructure.Data;
using MMWedding.Application.DependencyInjection;    
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
    Console.WriteLine("\t\tPRÜFUNG: Alle Komponente der Applicationschicht werden hinzugefügt...\n\n");
    builder.Services.AddApplication(builder.Configuration);

    Console.WriteLine("\t\tPRÜFUNG: Alle Komponente der Infrastruktur werden hinzugefügt...\n\n");

    builder.Services.AddInfrastructure(builder.Configuration);
    Console.WriteLine("\t\tERFOLG: Alle Komponenten der Infrastruktur wurden erfolgreich hinzugefügt.\n\n");
}
catch (Exception ex)
{
    if(builder.Services.AddInfrastructure(builder.Configuration) == null)
    {
        Console.WriteLine("\t\tFEHLER: Es gab ein Problem beim Hinzufügen der Infrastrukturkomponenten. Bitte überprüfen Sie die Konfiguration und die Implementierung der Infrastruktur.\n\n");
    }

    if(builder.Services.AddApplication(builder.Configuration) == null)
    {
        Console.WriteLine("\t\tFEHLER: Es gab ein Problem beim Hinzufügen der Anwendungsdienste. Bitte überprüfen Sie die Implementierung der Anwendungsdienste und deren Abhängigkeiten.\n\n");
    }
     Console.WriteLine($"\t\tFEHLER: {ex.Message}\n\n");

}

builder.Services.AddCors(options =>
{
    options.AddPolicy("BlazorClient", policy =>
    {
        policy.WithOrigins(
            "https://localhost:7165",
            "http://localhost:5097",
            "https://192.168.1.158:7165",
            "http://192.168.1.158:5097"  // ← Handy-Zugriff
        )
        .AllowAnyMethod()
        .AllowAnyHeader();
    });
});


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

app.UseCors("BlazorClient");
app.MapControllers();

app.Run();
