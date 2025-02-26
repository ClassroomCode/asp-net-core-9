using EComm.Entities;
using Microsoft.EntityFrameworkCore;

namespace EComm.DataAccess;

public class ECommDb : DbContext, IECommDb
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseInMemoryDatabase("EComm");
    }

    public DbSet<Product> Products { get; set; }

    public async Task<IEnumerable<Product>> GetAllProducts()
    {
        return await Products.ToListAsync();
    }

    public async Task<Product?> GetProduct(int id)
    {
        return await Products.FindAsync(id);
    }

    public Task AddProduct(Product product)
    {
        throw new NotImplementedException();
    }

    public Task<bool> DeleteProduct(Product product)
    {
        throw new NotImplementedException();
    }

    public Task<bool> UpdateProduct(Product product)
    {
        throw new NotImplementedException();
    }
}
