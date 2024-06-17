using System.ComponentModel.DataAnnotations;

namespace WebStore.Identity.API.ViewModels
{
    public class CustomerViewModel
    {
        [Required(ErrorMessage = "Field {0} is mandatory")]
        [StringLength(100, ErrorMessage = "The {0} field must be between {2} and {1} characters", MinimumLength = 2)]
        public string? FirstName { get; set; }

        [Required(ErrorMessage = "Field {0} is mandatory")]
        [StringLength(14, ErrorMessage = "The {0} field must be between {2} and {1} characters", MinimumLength = 2)]
        public string? LastName { get; set; }

        [Required(ErrorMessage = "Field {0} is mandatory")]
        [EmailAddress(ErrorMessage = "Field {0} is in an invalid format")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Field {0} is mandatory")]
        [StringLength(100, ErrorMessage = "The {0} field must be between {2} and {1} characters", MinimumLength = 6)]
        public string Password { get; set; }

        [Compare("Password", ErrorMessage = "Passwords don't match.")]
        public string PasswordConfirmation { get; set; }

        public string? Address { get; set; }
    }

    public class CustomerLogin
    {
        [Required(ErrorMessage = "Field {0} is mandatory")]
        [EmailAddress(ErrorMessage = "Field {0} is in an invalid format")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Field {0} is mandatory")]
        [StringLength(100, ErrorMessage = "The {0} field must be between {2} and {1} characters", MinimumLength = 6)]
        public string Senha { get; set; }
    }
}
