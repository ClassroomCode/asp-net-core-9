using EComm.BlazorUI.Client.Pages;
using EComm.BlazorUI.Components;
using EComm.DataAccess;
using EComm.Entities;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<HttpClient>(sp => new HttpClient
{
    BaseAddress = new Uri("http://localhost:5000")
});

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents()
    .AddInteractiveWebAssemblyComponents();

var connStr = builder.Configuration.GetConnectionString("EComm");
if (connStr is null) throw new ApplicationException("Database connection string not found");

builder.Services.AddScoped<IECommDb>(_ => ECommDbFactory.Create(connStr));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();


app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode()
    .AddInteractiveWebAssemblyRenderMode()
    .AddAdditionalAssemblies(typeof(EComm.BlazorUI.Client._Imports).Assembly);

app.Run();
