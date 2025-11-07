using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using CarMechanicWorkshop.Client;
using CarMechanicWorkshop.Client.Services;
using Microsoft.JSInterop;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

// builder.Services.AddScoped(sp =>
// {
//     var js = sp.GetRequiredService<IJSRuntime>();
//     var backendApiUrl = Environment.GetEnvironmentVariable("BACKEND_API");
//     System.Console.WriteLine("BACKEND_API environment variable: " + backendApiUrl);
//     if (string.IsNullOrEmpty(backendApiUrl))
//     {
//         System.Console.WriteLine("BACKEND_API environment variable not set, trying to get from appConfig...");
//         backendApiUrl = js.InvokeAsync<string>("eval", "window.appConfig.backendUrl").Result ?? builder.HostEnvironment.BaseAddress;
//         System.Console.WriteLine("Using backend URL from appConfig: " + backendApiUrl);
//     }
//     return new HttpClient { BaseAddress = new Uri($"{backendApiUrl.TrimEnd('/')}/api/") };
// });

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("http://localhost:8080/api/") });

builder.Services.AddScoped<ClientsApiService>();
builder.Services.AddScoped<JobsApiService>();

await builder.Build().RunAsync();
