﻿using FluentValidation;

namespace WebStore.Business.Models.Validations
{
    public class SupplierValidation : AbstractValidator<Customer>
    {
        public SupplierValidation()
        {
            RuleFor(f => f.FirstName)
                .NotEmpty().WithMessage("{PropertyName} field needs to be provided")
                .Length(2, 100).WithMessage("The {PropertyName} field must be between {MinLength} and {MaxLength} characters");

            RuleFor(f => f.LastName)
                .NotEmpty().WithMessage("{PropertyName} field needs to be provided")
                .Length(2, 100).WithMessage("The {PropertyName} field must be between {MinLength} and {MaxLength} characters");
           
        }
    }
}
