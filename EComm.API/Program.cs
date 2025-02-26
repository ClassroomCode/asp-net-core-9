using EComm.API;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ECommDb>(opt =>
    opt.UseInMemoryDatabase("EComm"));

var app = builder.Build();

app.MapGet("/products", async (ECommDb db) => 
    await db.Products.ToListAsync());

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