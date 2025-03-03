using EComm.Entities;

namespace EComm.Tests;

internal class ECommDbStub : IECommDb
{
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

    public async Task<Product?> GetProduct(int id)
    {
        return await Task.Run(() =>
            _products.SingleOrDefault(t => t.Id == id));
    }

    public Task<bool> UpdateProduct(Product product)
    {
        throw new NotImplementedException();
    }

    private static List<Product> _products = new List<Product> {
        new Product {
            Id = 1,
            ProductName = "Bread",
            UnitPrice = (decimal?)2.50,
            Package = "Bag",
            IsDiscontinued = false
        },
        new Product {
            Id = 2,
            ProductName = "Soup",
            UnitPrice = (decimal?)1.30,
            Package = "Can",
            IsDiscontinued = false
        }
    };
}
