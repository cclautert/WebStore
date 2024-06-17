using System.ComponentModel.DataAnnotations;

namespace WebStore.Identity.API.ViewModels
{
    public class UserViewModel
    {
        [Required(ErrorMessage = "Field {0} is mandatory")]
        [StringLength(100, ErrorMessage = "The {0} field must be between {2} and {1} characters", MinimumLength = 2)]
        public string? Name { get; set; }

        [Required(ErrorMessage = "Field {0} is mandatory")]
        [EmailAddress(ErrorMessage = "Field {0} is in an invalid format")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Field {0} is mandatory")]
        [StringLength(100, ErrorMessage = "Field {0} is in an invalid format", MinimumLength = 6)]
        public string Password { get; set; }
    }

    public class UserToken
    {
        public string Token { get; set; }
    }
}
