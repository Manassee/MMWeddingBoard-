using MMWeddingBoard.Infrastructure.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

try
{
    Console.WriteLine("\t\tPRÜFUNG: Alle Komponente der Infrastruktur werden hinzugefügt...\n\n");

    builder.Services.AddInfrastructure();
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

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
