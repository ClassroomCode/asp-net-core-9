using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.Services.AddScoped<HttpClient>(sp => new HttpClient { 
    BaseAddress = new Uri("http://localhost:5000") });

await builder.Build().RunAsync();
