using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Moq;
using WebStore.Business.Interfaces;
using WebStore.Business.Models;
using WebStore.Identity.API.Controllers;
using WebStore.Identity.API.ViewModels;

namespace WebStore.Identity.API.Tests.Controller
{
    public class UserControllerTest
    {
        [Fact]
        public void TeamController_Search_Get_Valid()
        {
            // Arrange
            var mockRepo = new Mock<IUserRepository>();
            var mockService = new Mock<IUserService>();
            var mockMapper = new Mock<IMapper>();
            var mockNotifier = new Mock<INotifier>();
            var controller = new UserController(mockMapper.Object, mockRepo.Object, mockService.Object, mockNotifier.Object);

            // Act
            var result = controller.GetAll() as Task<IEnumerable<UserViewModel>>;

            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public async Task Create_Returns_Http_Action_Result()
        {
            // Arrange
            Guid testSessionId = new Guid();
            var mockRepo = new Mock<IUserRepository>();
            mockRepo.Setup(repo => repo.GetByIdAsync(testSessionId))!
                .ReturnsAsync((User)null!);
            var mockService = new Mock<IUserService>();
            var mockMapper = new Mock<IMapper>();
            var mockNotifier = new Mock<INotifier>();
            var controller = new UserController(mockMapper.Object, mockRepo.Object, mockService.Object, mockNotifier.Object);

            // Act
            var result = await controller.Register(new UserViewModel());

            // Assert
            Assert.IsType<ActionResult<UserToken>>(result);
        }

        [Fact]
        public void Index_Returns_ViewResult_WithAListOfBrainstormSessions()
        {
            // Arrange
            var mockRepo = new Mock<IUserRepository>();
            var mockService = new Mock<IUserService>();
            var mockMapper = new Mock<IMapper>();
            var mockNotifier = new Mock<INotifier>();
            var controller = new UserController(mockMapper.Object, mockRepo.Object, mockService.Object, mockNotifier.Object);

            // Act
            var result = controller.GetAll() as Task<IEnumerable<UserViewModel>>;

            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public void Index_ReturnsAViewResult_WithAListOfBrainstormSessions()
        {
            // Arrange
            var mockRepo = new Mock<IUserRepository>();
            var mockService = new Mock<IUserService>();
            var mockMapper = new Mock<IMapper>();
            var mockNotifier = new Mock<INotifier>();
            var controller = new UserController(mockMapper.Object, mockRepo.Object, mockService.Object, mockNotifier.Object);
            
            // Act
            var result = controller.GetAll();

            // Assert
            var viewResult = Assert.IsType<Task<IEnumerable<UserViewModel>>>(result);
            Assert.Equal(0, viewResult.Result.Count());
        }

        private List<User> GetTestSessions()
        {
            var lstUser = new List<User>();
            lstUser.Add(new User()
            {
                Id = new Guid(),
                Name = "Test One",
                Email = "Test One",
                Password = "Test One",
            });
            lstUser.Add(new User()
            {
                Id = new Guid(),
                Name = "Test two",
                Email = "Test two",
                Password = "Test two",
            });
            return lstUser;
        }
    }
}