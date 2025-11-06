using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using CarMechanicWorkshop.Client;
using CarMechanicWorkshop.Client.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

var backendApiUrl = Environment.GetEnvironmentVariable("BackendApiUrl")
                     ?? builder.HostEnvironment.BaseAddress; // fallback for local dev which is not used

builder.Services.AddScoped(sp => new HttpClient
{
    BaseAddress = new Uri($"{backendApiUrl.TrimEnd('/')}/api/")
});


builder.Services.AddScoped<ClientsApiService>();

await builder.Build().RunAsync();
