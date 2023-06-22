using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using gcsb.ecommerce.domain.Validator.Product;

namespace gcsb.ecommerce.domain.Validator.OrderProduct
{
    public class OrderProductValidator : AbstractValidator<domain.OrderProduct.OrderProduct>
    {
        public OrderProductValidator()
        {
            RuleFor(e => e.Product)
            .NotNull()
            .NotEmpty()
            .WithMessage("The Product property cannot be null.");
            RuleFor(e=>e.Product)
            .SetValidator(new ProductValidator());
            RuleFor(e => e.IdOrder)
            .NotNull()
            .NotEmpty()
            .WithMessage("The IdOrder property cannot be null.");
            RuleFor(e => e.Amount)
            .NotNull()
            .NotEmpty()
            .WithMessage("The Quantity property cannot be null.")
            .GreaterThan(0)
            .WithMessage("The quantity must be greater than 0.");
            RuleFor(e=> e.Id)
            .NotNull()
            .NotEmpty()
            .WithMessage("The Id property cannot be null.");
        }
    }
}