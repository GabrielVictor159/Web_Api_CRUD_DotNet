using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
namespace gcsb.ecommerce.domain.Validator.Client
{
    public class ClientValidator : AbstractValidator<domain.Client.Client>
    {
        public ClientValidator()
        {
            RuleFor(e => e.Role)
                .NotNull()
                .NotEmpty()
                .Must(role => Enum.TryParse(typeof(domain.Enums.Policies), role, out _))
                .WithMessage("The Role property must be a valid value from the Policies enum.");
            RuleFor(e => e.Id)
            .NotNull()
            .NotEmpty()
            .WithMessage("The Id field is required.");
            RuleFor(e => e.Name)
            .NotNull()
            .MinimumLength(8)
            .WithMessage("The Name property must be at least 8 digits long.");
        }
    }
}