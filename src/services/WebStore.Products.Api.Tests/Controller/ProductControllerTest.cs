using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Moq;
using WebStore.Business.Interfaces;
using WebStore.Business.Models;
using WebStore.Products.Api.Controllers;
using WebStore.Products.Api.ViewModels;

namespace WebStore.Products.Api.Tests.Controller
{
    public class ProductControllerTest
    {
        [Fact]
        public void TeamController_Search_Get_Valid()
        {
            // Arrange
            var mockRepo = new Mock<IProductRepository>();
            var mockService = new Mock<IProductService>();
            var mockMapper = new Mock<IMapper>();
            var mockNotifier = new Mock<INotifier>();
            var controller = new ProductController(mockRepo.Object, mockService.Object, mockMapper.Object, mockNotifier.Object);

            // Act
            var result = controller.GetAll() as Task<IEnumerable<ProductIdViewModel>>;

            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public async Task Create_Returns_Http_Action_Result()
        {
            // Arrange
            Guid testSessionId = new Guid();
            var mockRepo = new Mock<IProductRepository>();
            mockRepo.Setup(repo => repo.GetByIdAsync(testSessionId))!
                .ReturnsAsync((Product)null!);
            var mockService = new Mock<IProductService>();
            var mockMapper = new Mock<IMapper>();
            var mockNotifier = new Mock<INotifier>();
            var controller = new ProductController(mockRepo.Object, mockService.Object, mockMapper.Object, mockNotifier.Object);

            // Act
            var result = await controller.Create(new ProductViewModel());

            // Assert
            Assert.IsType<ActionResult<ProductViewModel>>(result);
        }

        [Fact]
        public void Index_Returns_ViewResult_WithAListOfBrainstormSessions()
        {
            // Arrange
            var mockRepo = new Mock<IProductRepository>();
            var mockService = new Mock<IProductService>();
            var mockMapper = new Mock<IMapper>();
            var mockNotifier = new Mock<INotifier>();
            var controller = new ProductController(mockRepo.Object, mockService.Object, mockMapper.Object, mockNotifier.Object);

            // Act
            var result = controller.GetAll() as Task<IEnumerable<ProductIdViewModel>>;

            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public void Index_ReturnsAViewResult_WithAListOfBrainstormSessions()
        {
            // Arrange
            var mockRepo = new Mock<IProductRepository>();
            var mockService = new Mock<IProductService>();
            var mockMapper = new Mock<IMapper>();
            var mockNotifier = new Mock<INotifier>();
            var controller = new ProductController(mockRepo.Object, mockService.Object, mockMapper.Object, mockNotifier.Object);
            
            // Act
            var result = controller.GetAll();

            // Assert
            var viewResult = Assert.IsType<Task<IEnumerable<ProductViewModel>>>(result);
            Assert.Equal(0, viewResult.Result.Count());
        }

        private List<Product> GetTestSessions()
        {
            var lstProduct = new List<Product>();
            lstProduct.Add(new Product()
            {
                Id = new Guid(),
                Name = "Test One",
                Description = "Test One",
                Value = 1,
                DateRegister = new DateTime(2016, 7, 2),
            });
            lstProduct.Add(new Product()
            {
                Id = new Guid(),
                Name = "Test Two",
                Description = "Test Two",
                Value = 1,
                DateRegister = new DateTime(2016, 7, 2),
            });
            return lstProduct;
        }
    }
}