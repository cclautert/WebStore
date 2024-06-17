
namespace WebStore.Business.Models
{
    public class Product : Entity
    {
        public string Name { get; set; }

        public string? Description { get; set; }

        public decimal Value { get; set; }

        public DateTime DateRegister { get; set; }

        public Product()
        {
            
        }

        public Product(string name, string? description, decimal value, DateTime dateRegister)
        {
            Name = name;
            Description = description;
            Value = value;
            DateRegister = dateRegister;
        }
    }
}
