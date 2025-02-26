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

    public Task AddProduct(Product product)
    {
        throw new NotImplementedException();
    }

    public Task<bool> DeleteProduct(Product product)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Product>> GetAllProducts()
    {
        throw new NotImplementedException();
    }

    public Task<Product?> GetProduct(int id)
    {
        throw new NotImplementedException();
    }

    public Task<bool> UpdateProduct(Product product)
    {
        throw new NotImplementedException();
    }
}
