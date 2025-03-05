namespace EComm.Entities;

public interface IECommDb
{
    Task<IEnumerable<Product>> GetAllProducts(bool includeCategories = false);
    Task<Product?> GetProduct(int id);
    Task AddProduct(Product product);
    Task<bool> UpdateProduct(Product product);
    Task<bool> DeleteProduct(Product product);
}
