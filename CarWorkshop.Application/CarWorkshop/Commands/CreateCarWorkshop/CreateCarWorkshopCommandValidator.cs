using CarWorkshop.Domain.Interfaces;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarWorkshop.Application.CarWorkshop.Commands.CreateCarWorkshop
{
    public class CreateCarWorkshopCommandValidator : AbstractValidator<CreateCarWorkshopCommand>
    {
        public CreateCarWorkshopCommandValidator(ICarWorkshopRepository repository)
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage("Name is required")
                .MinimumLength(2).WithMessage("Name mus have atleast two  chars")
                .MaximumLength(20)
                .WithMessage("Name must have max 20 chararacters");
            RuleFor(x => x.Description)
                .MinimumLength(10)
                .WithMessage("Description need minimum 20 characters");
            RuleFor(x => x.PhoneNumber)
                .MinimumLength(8).WithMessage("Phone Number must have beetwen 8 - 12 numbers")
                .MaximumLength(12).WithMessage("Phone Number must have beetwen 8 - 12 numbers");

            RuleFor(x => x.Name).Custom((value, context) =>
            {
                var nameInUse = repository.GetName(value).Result;
                if (nameInUse != null)
                {
                    context.AddFailure("Name", "Name is already in use \n Please change your Car Workshop Name");
                }
            });
        }
    }
}
