global using EComm.Entities;
using EComm.DataAccess;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ECommDb>(opt =>
    opt.UseInMemoryDatabase("EComm"));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddOpenApiDocument(config =>
{
    config.DocumentName = "ECommAPI";
    config.Title = "ECommAPI v1";
    config.Version = "v1";
});

var app = builder.Build();

app.UseOpenApi();

app.MapGet("/products", async (ECommDb db) => 
    await db.Products.ToArrayAsync());

app.MapGet("/products/{id}", async (int id, ECommDb db) =>
    await db.Products.FindAsync(id) is Product product
        ? Results.Ok(product)
        : Results.NotFound());

using (var serviceScope = app.Services.CreateScope())
{
    var services = serviceScope.ServiceProvider;
    var db = services.GetRequiredService<ECommDb>();
    db.Products.Add(new Product
    {
        Id = 1,
        ProductName = "Bread",
        UnitPrice = (decimal?)2.50,
        Package = "Bag",
        IsDiscontinued = false
    });
    db.Products.Add(new Product
    {
        Id = 2,
        ProductName = "Soup",
        UnitPrice = (decimal?)1.30,
        Package = "Can",
        IsDiscontinued = false
    });
    await db.SaveChangesAsync();
}

app.Run();