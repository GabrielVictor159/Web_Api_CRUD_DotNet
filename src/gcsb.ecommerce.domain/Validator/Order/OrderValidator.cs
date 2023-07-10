using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using gcsb.ecommerce.domain.Enums;
using gcsb.ecommerce.domain.Validator.OrderProduct;

namespace gcsb.ecommerce.domain.Validator.Order
{
    public class OrderValidator : AbstractValidator<domain.Order.Order>
    {
        public OrderValidator()
        {
        RuleFor(order => order.Id)
            .NotEqual(Guid.Empty)
            .WithMessage("The Id field is required.");
        RuleFor(order => order.IdClient)
            .NotEqual(Guid.Empty)
            .WithMessage("The IdClient field is required.");
        RuleFor(order => order.TotalOrder)
            .GreaterThan(0)
            .WithMessage("The TotalOrder field must be greater than zero.");
        RuleFor(order => order.OrderDate)
            .LessThanOrEqualTo(DateTime.UtcNow)
            .WithMessage("The OrderDate field must be a date earlier than the current date.");
        RuleFor(order => order.ListOrderProduct)
             .NotNull()
             .NotEmpty()
             .WithMessage("Product list must have valid items.");
        RuleForEach(order => order.ListOrderProduct)
                .SetValidator(new OrderProductValidator());
        RuleFor(order => order.Cupons)
                .Must(cupom => string.IsNullOrEmpty(cupom) || Enum.IsDefined(typeof(Cupons), cupom))
                .When(cupom => !string.IsNullOrEmpty(cupom.Cupons))
                .WithMessage("Cupom inválido. Por favor, insira um cupom válido do enum Cupons.");
        }
    }
}