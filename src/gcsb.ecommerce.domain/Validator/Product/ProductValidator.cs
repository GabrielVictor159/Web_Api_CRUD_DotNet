using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;

namespace gcsb.ecommerce.domain.Validator.Product
{
    public class ProductValidator : AbstractValidator<domain.Product.Product>
    {
        public ProductValidator()
        {
            RuleFor(product => product.Id)
            .NotNull()
            .NotEmpty()
            .WithMessage("The Id property cannot be null or empty.");
            RuleFor(product => product.Name)
            .NotNull()
            .NotEmpty()
            .WithMessage("The Name property cannot be null or empty.");
            RuleFor(product => product.Value)
            .NotNull()
            .GreaterThan(0)
            .WithMessage("The Value property must be a valid value greater than 0."); 
        }
    }
}