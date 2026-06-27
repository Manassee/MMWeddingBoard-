using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MMWeddingBoard;
using MudBlazor.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");
builder.Services.AddMudServices();

var apiBase = builder.HostEnvironment.IsDevelopment()
    ? "http://localhost:5008/"
    : "http://192.168.1.158:5008/";

builder.Services.AddScoped(sp => new HttpClient
{
    BaseAddress = new Uri(apiBase)
});




await builder.Build().RunAsync();
