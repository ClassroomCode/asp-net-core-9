global using EComm.Entities;
using EComm.DataAccess;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

var connStr = builder.Configuration.GetConnectionString("EComm");
if (connStr is null) throw new ApplicationException("Database connection string not found");

builder.Services.AddScoped<IECommDb>(_ => ECommDbFactory.Create(connStr));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddOpenApiDocument(config =>
{
    config.DocumentName = "ECommAPI";
    config.Title = "ECommAPI v1";
    config.Version = "v1";
});

var app = builder.Build();

app.UseOpenApi();

app.MapControllers();


/*
app.MapGet("/products", async (IECommDb db) =>
    await db.GetAllProducts());

app.MapGet("/products/{id}", async (int id, IECommDb db) =>
    await db.GetProduct(id) is Product product
        ? Results.Ok(product)
        : Results.NotFound());
*/

/*
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
*/

app.Run();