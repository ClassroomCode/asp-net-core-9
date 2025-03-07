global using EComm.Entities;
using EComm.DataAccess;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();

var connStr = builder.Configuration.GetConnectionString("EComm");
if (connStr is null) throw new ApplicationException("Database connection string not found");

builder.Services.AddScoped<IECommDb>(_ => ECommDbFactory.Create(connStr));

var app = builder.Build();

app.MapStaticAssets();
app.MapRazorPages()
   .WithStaticAssets();

app.Run();