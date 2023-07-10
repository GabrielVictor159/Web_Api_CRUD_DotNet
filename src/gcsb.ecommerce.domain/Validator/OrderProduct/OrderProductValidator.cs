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
        }
    }
}