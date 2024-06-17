using FluentValidation;

namespace WebStore.Business.Models.Validations
{
    public class UserValidation : AbstractValidator<User>
    {
        public UserValidation()
        {
            RuleFor(f => f.Name)
                .NotEmpty().WithMessage("{PropertyName} field needs to be provided")
                .Length(2, 100).WithMessage("The {PropertyName} field must be between {MinLength} and {MaxLength} characters");

            RuleFor(f => f.Email)
                .NotEmpty().WithMessage("{PropertyName} field needs to be provided")
                .Length(2, 100).WithMessage("The {PropertyName} field must be between {MinLength} and {MaxLength} characters");
        }
    }
}
