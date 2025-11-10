using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using CarMechanicWorkshop.Client;
using CarMechanicWorkshop.Client.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

// Read backend URL from environment or fallback to same origin
var backendUrl = builder.Configuration["BackendApiUrl"] 
                 ?? Environment.GetEnvironmentVariable("BACKEND_API") 
                 ?? builder.HostEnvironment.BaseAddress + "api/";

Console.WriteLine($"builder.Configuration[\"BackendApiUrl\"] | {builder.Configuration["BackendApiUrl"]}");
Console.WriteLine($"Environment.GetEnvironmentVariable(\"BACKEND_API\") | {Environment.GetEnvironmentVariable("BACKEND_API")}");
Console.WriteLine($"builder.HostEnvironment.BaseAddress | {builder.HostEnvironment.BaseAddress}");

// Register HttpClient
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(backendUrl ?? "http://localhost:8080/api/") });

// Register typed services
builder.Services.AddScoped<ClientsApiService>();
builder.Services.AddScoped<JobsApiService>();

await builder.Build().RunAsync();
