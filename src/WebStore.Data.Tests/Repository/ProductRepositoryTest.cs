using Moq;
using WebStore.Business.Interfaces;
using WebStore.Business.Models;

namespace WebStore.Data.Tests.Repository
{
    public class ProductRepositoryTest
    {
        [Fact]
        public async Task Should_Create_Product_When_Command_Valid()
        {
            //Arrange
            string? name = "Test";
            string? description = "Product";
            decimal value = 100;
            DateTime dateRegister = DateTime.Now;

            //Act
            Product product = new Product(name, description, value, dateRegister);
            var productRepository = new Mock<IProductRepository>();

            var result = productRepository.Object.CreateAsync(product);

            //Assert
            Assert.NotNull(result);
            Assert.Equal(result.IsCompleted, true);
        }

        [Fact]
        public async Task Should_Update_Product_When_Command_Valid()
        {
            //Arrange
            string? name = "Test";
            string? description = "Product";
            decimal value = 100;
            DateTime dateRegister = DateTime.Now;

            //Act
            Product product = new Product(name, description, value, dateRegister);
            var productRepository = new Mock<IProductRepository>();

            var result = productRepository.Object.UpdateAsync(product);

            //Assert
            Assert.NotNull(result);
            Assert.Equal(result.IsCompleted, true);
        }

        [Fact]
        public void Remove_Product_With_Sucess()
        {
            //Arrange
            Guid id = new Guid();

            //Act
            var productRepository = new Mock<IProductRepository>();
            var result = productRepository.Object.RemoveAsync(id);

            //Assert
            Assert.NotNull(result);
            Assert.Equal(result.IsCompleted, true);
        }

        [Fact]
        public void GetById_Product_With_Sucess()
        {
            //Arrange
            Guid id = new Guid();

            //Act
            var productRepository = new Mock<IProductRepository>();
            var result = productRepository.Object.GetByIdAsync(id);

            //Assert
            Assert.NotNull(result);
            Assert.Equal(result.IsCompleted, true);
        }

        [Fact]
        public void GetAll_Product_With_Sucess()
        {
            //Arrange

            //Act
            var productRepository = new Mock<IProductRepository>();
            var result = productRepository.Object.GetAllAsync();

            //Assert
            Assert.NotNull(result);
            Assert.Equal(result.IsCompleted, true);
        }
    }
}