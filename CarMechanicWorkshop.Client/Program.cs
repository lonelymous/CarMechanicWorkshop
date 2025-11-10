using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using CarMechanicWorkshop.Client;
using CarMechanicWorkshop.Client.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

// Register HttpClient
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("http://localhost:8080/api/") });

// Register typed services
builder.Services.AddScoped<ClientsApiService>();
builder.Services.AddScoped<JobsApiService>();

await builder.Build().RunAsync();
