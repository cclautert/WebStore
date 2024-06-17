using System.ComponentModel.DataAnnotations;
using WebStore.Business.Models;

namespace WebStore.Business.Tests.Models
{
    public class CustomerTest
    {
        [Fact]
        public void Create_Customer_With_Sucess()
        {
            //Arrange
            string? firstName = "First";
            string? lastName = "Last";
            string? email = "last@gmail.com";
            string? password = "123456";
            string? address = "Florida";

            //Act
            var customer = new Customer(firstName, lastName, email, password, address);

            //Assert
            Assert.Equal(customer.FirstName, firstName);
            Assert.Equal(customer.LastName, lastName);
            Assert.Equal(customer.Email, email);
            Assert.Equal(customer.Password, password);
            Assert.Equal(customer.Address, address);
        }

        public static TheoryData<string?, string?, string?, string?, string?, bool> Cases =
            new()
            {
                { "First", "Last", "email", "pass", "address", true },
                { "First", null, "email", "pass", "address", true },
                { "First", "Last", null, "pass", "address", true },
                { "First", "Last", "email", null, "address", true },
                { "First", "Last", "email", "pass", null, true },
            };

        [Theory, MemberData(nameof(Cases))]
        public void Test_Model_Customer_With_Sucess(string fisrtName, string lastName, string? email, string password, string? address, bool isValid)
        {
            //Arrange

            //Act
            Customer customer = new Customer(fisrtName, lastName, email, password, address);

            //Assert
            Assert.Equal(isValid, ValidateModel(customer));
        }

        private bool ValidateModel(object model)
        {
            var validationResults = new List<ValidationResult>();
            var ctx = new ValidationContext(model, null, null);
 
            return Validator.TryValidateObject(model, ctx, validationResults, true);
        }
    }
}