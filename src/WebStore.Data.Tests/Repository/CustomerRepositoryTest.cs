using Moq;
using WebStore.Business.Interfaces;
using WebStore.Business.Models;

namespace WebStore.Data.Tests.Repository
{
    public class CustomerRepositoryTest
    {
        [Fact]
        public async Task Should_Create_Customer_When_Command_Valid()
        {
            //Arrange
            string? firstName = "First";
            string? lastName = "Last";
            string? email = "last@gmail.com";
            string? password = "123456";
            string? address = "Florida";

            //Act
            var customerRepository = new Mock<ICustomerRepository>();
            var customer = new Customer(firstName, lastName, email, password, address);
            var result = customerRepository.Object.CreateAsync(customer);

            //Assert
            Assert.NotNull(result);
            Assert.Equal(result.IsCompleted, true);
        }

        [Fact]
        public async Task Should_Update_Customer_When_Command_Valid()
        {
            //Arrange
            string? firstName = "First";
            string? lastName = "Last";
            string? email = "last@gmail.com";
            string? password = "123456";
            string? address = "Florida";

            //Act
            var customerRepository = new Mock<ICustomerRepository>();
            var customer = new Customer(firstName, lastName, email, password, address);
            var result = customerRepository.Object.UpdateAsync(customer);

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
            var customerRepository = new Mock<ICustomerRepository>();
            var result = customerRepository.Object.RemoveAsync(id);

            //Assert
            Assert.NotNull(result);
            Assert.Equal(result.IsCompleted, true);
        }

        [Fact]
        public void GetById_Customer_With_Sucess()
        {
            //Arrange
            Guid id = new Guid();

            //Act
            var customerRepository = new Mock<ICustomerRepository>();
            var result = customerRepository.Object.GetByIdAsync(id);

            //Assert
            Assert.NotNull(result);
            Assert.Equal(result.IsCompleted, true);
        }

        [Fact]
        public void SignIn_Customer_With_Sucess()
        {
            //Arrange
            string email = "test@test.com";
            string password = "123456";

            //Act
            var customerRepository = new Mock<ICustomerRepository>();
            var result = customerRepository.Object.PasswordSignInAsync(email, password);

            //Assert
            Assert.NotNull(result);
            Assert.Equal(result.IsCompleted, true);
        }

        [Fact]
        public void GetAll_Customer_With_Sucess()
        {
            //Arrange

            //Act
            var customerRepository = new Mock<ICustomerRepository>();
            var result = customerRepository.Object.GetAllAsync();

            //Assert
            Assert.NotNull(result);
            Assert.Equal(result.IsCompleted, true);
        }
    }
}