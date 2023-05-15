using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using Web_Api_CRUD.Domain.DTO;

namespace API.Domain.DTO
{
    public class ProdutoQuantidadeDTOValidation : AbstractValidator<ProdutoQuantidadeDTO>
    {
        public ProdutoQuantidadeDTOValidation()
        {
            RuleFor(dto => dto.Produto)
            .NotNull()
            .NotEmpty()
            .WithMessage("A propriedade Produto não pode ser nula.");

            RuleFor(dto => dto.Quantidade)
            .NotNull()
            .NotEmpty()
            .WithMessage("A propriedade Quantidade não pode ser nula.")
            .GreaterThan(0)
            .WithMessage("A quantidade deve ser maior que 0.");
        }
    }
}