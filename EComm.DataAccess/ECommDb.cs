using EComm.Entities;
using Microsoft.EntityFrameworkCore;

namespace EComm.DataAccess;

internal class ECommDb : DbContext, IECommDb
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseInMemoryDatabase("EComm");
    }

    public DbSet<Product> Products { get; set; }

    public async Task<IEnumerable<Product>> GetAllProducts()
    {
        return await Products.AsNoTracking().ToListAsync();    
    }

    public async Task<Product?> GetProduct(int id)
    {
        return await Products.FindAsync(id);
    }

    public async Task AddProduct(Product product)
    {
        Products.Add(product);
        await SaveChangesAsync();
    }

    public async Task<bool> DeleteProduct(Product product)
    {
        Products.Remove(product);
        var rowsAffected = await SaveChangesAsync();
        return (rowsAffected > 0);
    }

    public async Task<bool> UpdateProduct(Product product)
    {
        var rowsAffected = await SaveChangesAsync();
        return (rowsAffected > 0);
    }
}
