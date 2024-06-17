using Moq;
using WebStore.Business.Interfaces;
using WebStore.Business.Models;
using WebStore.Business.Services;

namespace WebStore.Business.Tests.Services
{
    public class ProductServiceTest
    {
        [Fact]
        public void Create_Product_With_Sucess()
        {
            //Arrange
            string? name = "Test";
            string? description = "Product";
            decimal value = 100;
            DateTime dateRegister = DateTime.Now;

            //Act
            Product product = new Product(name, description, value, dateRegister);
            var productRepository = new Mock<IProductRepository>();
            var notifier = new Mock<INotifier>();
            ProductService productService = new ProductService(productRepository.Object, notifier.Object);

            var result = productService.CreateAsync(product);

            //Assert
            Assert.NotNull(result);
            Assert.Equal(result.IsCompleted, true);
        }

        [Fact]
        public void Update_Customer_With_Sucess()
        {
            //Arrange
            string? name = "Test";
            string? description = "Product";
            decimal value = 100;
            DateTime dateRegister = DateTime.Now;

            //Act
            Product product = new Product(name, description, value, dateRegister);
            var productRepository = new Mock<IProductRepository>();
            var notifier = new Mock<INotifier>();
            ProductService productService = new ProductService(productRepository.Object, notifier.Object);

            var result = productService.UpdateAsync(product);

            //Assert
            Assert.NotNull(result);
            Assert.Equal(result.IsCompleted, true);
        }

        [Fact]
        public void Remove_Customer_With_Sucess()
        {
            //Arrange
            Guid id = new Guid();

            //Act
            var productRepository = new Mock<IProductRepository>();
            var notifier = new Mock<INotifier>();
            ProductService productService = new ProductService(productRepository.Object, notifier.Object);

            var result = productService.RemoveAsync(id);

            //Assert
            Assert.NotNull(result);
            Assert.Equal(result.IsCompleted, true);
        }
    }
}