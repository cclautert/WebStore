using Moq;
using WebStore.Business.Interfaces;
using WebStore.Business.Models;
using WebStore.Business.Services;

namespace WebStore.Business.Tests.Services
{
    public class CustomerServiceTest
    {
        [Fact]
        public void Create_Customer_With_Sucess()
        {
            //Arrange
            string? firstName = "First";
            string? lastName = "Last";
            string? email = "last@gmail.com";
            string? address = "Florida";

            //Act
            var customer = new Customer(firstName, lastName, email, address);
            var customerRepository = new Mock<ICustomerRepository>();
            var notifier = new Mock<INotifier>();
            CustomerService customerService = new CustomerService(customerRepository.Object, notifier.Object);

            var result = customerService.CreateAsync(customer);

            //Assert
            Assert.NotNull(result);
            Assert.Equal(result.IsCompleted, true);
        }

        [Fact]
        public void Update_Customer_With_Sucess()
        {
            //Arrange
            string? firstName = "First";
            string? lastName = "Last";
            string? email = "last@gmail.com";
            string? address = "Florida";

            //Act
            var customer = new Customer(firstName, lastName, email, address);
            var customerRepository = new Mock<ICustomerRepository>();
            var notifier = new Mock<INotifier>();
            CustomerService customerService = new CustomerService(customerRepository.Object, notifier.Object);

            var result = customerService.UpdateAsync(customer);

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
            var notifier = new Mock<INotifier>();
            CustomerService customerService = new CustomerService(customerRepository.Object, notifier.Object);

            var result = customerService.RemoveAsync(id);

            //Assert
            Assert.NotNull(result);
            Assert.Equal(result.IsCompleted, true);
        }
    }
}