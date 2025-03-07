using EComm.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace EComm.DataAccess;

internal class ECommDb(string connStr) : DbContext, IECommDb
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql(connStr);
        optionsBuilder.LogTo(Console.WriteLine);
    }

    public DbSet<Product> Products { get; set; }
    public DbSet<Category> Categories { get; set; }

    public async Task<IEnumerable<Product>> GetAllProducts(bool includeCategories = false)
    {
        //await Task.Delay(5000);

        if (includeCategories)
        {
            return await Products.AsNoTracking()
                .Include(p => p.Category)
                .ToListAsync();
        }
        return await Products.AsNoTracking().ToListAsync();  
    }

    public async Task<Product?> GetProduct(int id, bool includeCategory = false)
    {
        if (includeCategory)
        {
            return await Products
                .Include(p => p.Category)
                .SingleOrDefaultAsync(p => p.Id == id);
        }
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

    public async Task<IEnumerable<Category>> GetAllCategories()
    {
        return await Categories.AsNoTracking().ToListAsync();
    }

    private const int PageSize = 2;

    public async Task<IEnumerable<Product>> GetAllProductsByPage(int startIndex = 0, bool includeCategories = false)
    {
        if (includeCategories)
        {
            return await Products.AsNoTracking()
                .Include(p => p.Category)
                .Skip(startIndex)
                .Take(PageSize)
                .ToListAsync();
        }
        return await Products.AsNoTracking().Skip(startIndex).Take(PageSize).ToListAsync();
    }

    public IQueryable<Product> DefProducts => Products;

    public async Task<IEnumerable<Product>> EnumerateProducts(IQueryable<Product> q)
    {
        return await q.ToListAsync();
    }
}
