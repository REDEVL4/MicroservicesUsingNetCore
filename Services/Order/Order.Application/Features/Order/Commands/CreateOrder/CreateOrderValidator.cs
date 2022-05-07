using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order.Application.Features.Order.Commands.CreateOrder
{
    public class CreateOrderValidator:AbstractValidator<CreateOrderCommand>
    {
        public CreateOrderValidator()
        {
            RuleFor(c => c.UserName)
                .NotEmpty().WithMessage("{UserName} is Required")
                .NotNull()
                .MaximumLength(50).WithMessage("{UserName} must be less than or equal to 50 characters long");
            RuleFor(c => c.FirstName)
                .NotEmpty().WithMessage("{FirstName} is Required");
            RuleFor(c => c.LastName)
                .NotEmpty().WithMessage("{LastName} is Required");
            RuleFor(c => c.TotalPrice)
                .NotEmpty().WithMessage("{TotalPrice} must be present")
                .GreaterThan(0).WithMessage("{TotalPrice} must be greater than 0");
        }
    }
}
