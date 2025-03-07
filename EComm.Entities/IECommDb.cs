namespace EComm.Entities;

public interface IECommDb
{
    Task<IEnumerable<Product>> GetAllProducts(bool includeCategories = false);
    Task<Product?> GetProduct(int id, bool includeCategory = false);
    Task AddProduct(Product product);
    Task<bool> UpdateProduct(Product product);
    Task<bool> DeleteProduct(Product product);

    Task<IEnumerable<Category>> GetAllCategories();

    Task<IEnumerable<Product>> GetAllProductsByPage(int startIndex = 0, bool includeCategories = false);
}
