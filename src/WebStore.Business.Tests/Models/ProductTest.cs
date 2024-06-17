using System.ComponentModel.DataAnnotations;
using WebStore.Business.Models;

namespace WebStore.Business.Tests.Models
{
    public class ProductTest
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

            //Assert
            Assert.Equal(product.Name, name);
            Assert.Equal(product.Description, description);
            Assert.Equal(product.Value, value);
            Assert.Equal(product.DateRegister, dateRegister);
        }

        public static TheoryData<string, string, decimal, DateTime, bool> Cases =
            new()
            {
                { null, null, 1, new DateTime(2017, 3, 1), true },
                { "", "", 1, new DateTime(2017, 3, 1), true },
                { "Name", "", 1, new DateTime(2017, 3, 1), true },
                { "", "Description", 1, new DateTime(2017, 3, 1), true },
                { "Name", "Description", 1, new DateTime(2017, 3, 1), true },
                { "Name", "Description", 0, new DateTime(2017, 3, 1), true }
            };

        [Theory, MemberData(nameof(Cases))]
        public void Test_Model_Product_With_Sucess(string? name, string? description, decimal value, DateTime dateRegister, bool isValid)
        {
            //Arrange

            //Act
            Product product = new Product(name, description, value, dateRegister);

            //Assert
            Assert.Equal(isValid, ValidateModel(product));
        }

        private bool ValidateModel(object model)
        {
            var validationResults = new List<ValidationResult>();
            var ctx = new ValidationContext(model, null, null);
 
            return Validator.TryValidateObject(model, ctx, validationResults, true);
        }
    }
}