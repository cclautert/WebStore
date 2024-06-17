using System.ComponentModel.DataAnnotations;

namespace WebStore.Identity.API.ViewModels
{
    public class CustomerLoginViewModel
    {
        [Required(ErrorMessage = "Field {0} is mandatory")]
        [EmailAddress(ErrorMessage = "Field {0} is in an invalid format")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Field {0} is mandatory")]
        [StringLength(100, ErrorMessage = "Field {0} is in an invalid format", MinimumLength = 6)]
        public string Password { get; set; }
    }
}
