using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Moq;
using WebStore.Business.Interfaces;
using WebStore.Business.Models;
using WebStore.Identity.API.Controllers;
using WebStore.Identity.API.ViewModels;

namespace WebStore.Identity.API.Tests.Controller
{
    public class CustomerControllerTest
    {
        [Fact]
        public void TeamController_Search_Get_Valid()
        {
            // Arrange
            var mockRepo = new Mock<ICustomerRepository>();
            var mockService = new Mock<ICustomerService>();
            var mockMapper = new Mock<IMapper>();
            var mockNotifier = new Mock<INotifier>();
            var controller = new CustomerController(mockMapper.Object, mockRepo.Object, mockService.Object, mockNotifier.Object);

            // Act
            var result = controller.GetAll() as Task<IEnumerable<CustomerIdViewModel>>;

            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public async Task Create_Returns_Http_Action_Result()
        {
            // Arrange
            Guid testSessionId = new Guid();
            var mockRepo = new Mock<ICustomerRepository>();
            mockRepo.Setup(repo => repo.GetByIdAsync(testSessionId))!
                .ReturnsAsync((Customer)null!);
            var mockService = new Mock<ICustomerService>();
            var mockMapper = new Mock<IMapper>();
            var mockNotifier = new Mock<INotifier>();
            var controller = new CustomerController(mockMapper.Object, mockRepo.Object, mockService.Object, mockNotifier.Object);

            // Act
            var result = await controller.Create(new CustomerViewModel());

            // Assert
            Assert.IsType<ActionResult<CustomerViewModel>>(result);
        }

        [Fact]
        public void Index_Returns_ViewResult_WithAListOfBrainstormSessions()
        {
            // Arrange
            var mockRepo = new Mock<ICustomerRepository>();
            var mockService = new Mock<ICustomerService>();
            var mockMapper = new Mock<IMapper>();
            var mockNotifier = new Mock<INotifier>();
            var controller = new CustomerController(mockMapper.Object, mockRepo.Object, mockService.Object, mockNotifier.Object);

            // Act
            var result = controller.GetAll() as Task<IEnumerable<CustomerIdViewModel>>;

            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public void Index_ReturnsAViewResult_WithAListOfBrainstormSessions()
        {
            // Arrange
            var mockRepo = new Mock<ICustomerRepository>();
            var mockService = new Mock<ICustomerService>();
            var mockMapper = new Mock<IMapper>();
            var mockNotifier = new Mock<INotifier>();
            var controller = new CustomerController(mockMapper.Object, mockRepo.Object, mockService.Object, mockNotifier.Object);
            
            // Act
            var result = controller.GetAll();

            // Assert
            var viewResult = Assert.IsType<Task<IEnumerable<CustomerIdViewModel>>>(result);
            Assert.Equal(0, viewResult.Result.Count());
        }

        private List<Customer> GetTestSessions()
        {
            var lstCustomer = new List<Customer>();
            lstCustomer.Add(new Customer()
            {
                Id = new Guid(),
                FirstName = "Test One",
                LastName = "Test One",
                Email = "Test One",
                Address = "Test One",
            });
            lstCustomer.Add(new Customer()
            {
                Id = new Guid(),
                FirstName = "Test two",
                LastName = "Test two",
                Email = "Test two",
                Address = "Test two",
            });
            return lstCustomer;
        }
    }
}