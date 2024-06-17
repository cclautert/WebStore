using Moq;
using WebStore.Business.Interfaces;
using WebStore.Business.Models;

namespace WebStore.Data.Tests.Repository
{
    public class UserRepositoryTest
    {
        [Fact]
        public async Task Should_Create_User_When_Command_Valid()
        {
            //Arrange
            string? Name = "Name";
            string? email = "last@gmail.com";
            string? password = "Florida";

            //Act
            var userRepository = new Mock<IUserRepository>();
            var user = new User(Name, email, password);
            var result = userRepository.Object.CreateAsync(user);

            //Assert
            Assert.NotNull(result);
            Assert.Equal(result.IsCompleted, true);
        }

        [Fact]
        public async Task Should_Update_User_When_Command_Valid()
        {
            //Arrange
            string? Name = "Name";
            string? email = "last@gmail.com";
            string? password = "Florida";

            //Act
            var userRepository = new Mock<IUserRepository>();
            var user = new User(Name, email, password);
            var result = userRepository.Object.UpdateAsync(user);

            //Assert
            Assert.NotNull(result);
            Assert.Equal(result.IsCompleted, true);
        }

        [Fact]
        public void Remove_User_With_Sucess()
        {
            //Arrange
            Guid id = new Guid();

            //Act
            var userRepository = new Mock<IUserRepository>();
            var result = userRepository.Object.RemoveAsync(id);

            //Assert
            Assert.NotNull(result);
            Assert.Equal(result.IsCompleted, true);
        }

        [Fact]
        public void GetById_User_With_Sucess()
        {
            //Arrange
            Guid id = new Guid();

            //Act
            var userRepository = new Mock<IUserRepository>();
            var result = userRepository.Object.GetByIdAsync(id);

            //Assert
            Assert.NotNull(result);
            Assert.Equal(result.IsCompleted, true);
        }
        [Fact]
        public void GetAll_User_With_Sucess()
        {
            //Arrange

            //Act
            var userRepository = new Mock<IUserRepository>();
            var result = userRepository.Object.GetAllAsync();

            //Assert
            Assert.NotNull(result);
            Assert.Equal(result.IsCompleted, true);
        }
    }
}