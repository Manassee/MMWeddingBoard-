var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var cs = builder.Configuration.GetConnectionString("DefaultConnection");
if(cs is null)
{
    throw new InvalidOperationException("Datenbankverbindung fail, Bitte prüfe den Connectionstring...\n");
}
else
{
    Console.WriteLine("ConnectionString wurde geladen.\nDatenbankverbindung erfolgreich hergestellt...\n ");
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
