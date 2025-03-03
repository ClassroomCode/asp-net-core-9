using EComm.API.Controllers;
using EComm.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging.Abstractions;
using System.Linq.Expressions;

namespace EComm.Tests
{
    public class UnitTest1
    {
        [Fact]
        public async Task Test1()
        {
            // Arrange
            var db = new ECommDbStub();
            var controller = new ProductController(new NullLogger<ProductController>(), db);

            // Act
            var r = controller.GetProduct(2);

            // Assert
            Assert.NotNull(r.Result);
            Assert.IsAssignableFrom<OkObjectResult>(r.Result);
            var objResult = (OkObjectResult)r.Result;
            Assert.NotNull(objResult.Value);
            Assert.IsAssignableFrom<Product>(objResult.Value);
            Product product = (Product)objResult.Value;
            Assert.Equal(2, product.Id);
        }
    }
}
