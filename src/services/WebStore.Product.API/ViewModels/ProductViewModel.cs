using System.ComponentModel.DataAnnotations;

namespace WebStore.Products.Api.ViewModels
{
    public class ProductViewModel
    {
        [Required(ErrorMessage = "Field {0} is mandatory")]
        [StringLength(200, ErrorMessage = "The {0} field must be between {2} and {1} characters", MinimumLength = 2)]
        public string? Name { get; set; }

        [Required(ErrorMessage = "Field {0} is mandatory")]
        [StringLength(1000, ErrorMessage = "The {0} field must be between {2} and {1} characters", MinimumLength = 2)]
        public string? Description { get; set; }

        [Required(ErrorMessage = "Field {0} is mandatory")]
        public decimal Value { get; set; }

        public DateTime DateRegister { get; set; }
    }
}
