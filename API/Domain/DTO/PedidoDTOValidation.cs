using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Domain.Enums;
using FluentValidation;
using Web_Api_CRUD.Domain.DTO;

namespace API.Domain.DTO
{
    public class PedidoDTOValidation : AbstractValidator<PedidoDTO>
    {
        public PedidoDTOValidation()
        {
            RuleFor(dto => dto.listaProdutos)
             .NotNull()
             .NotEmpty()
             .WithMessage("A lista de produtos deve ter itens válidos");

            RuleForEach(dto => dto.listaProdutos)
                .SetValidator(new ProdutoQuantidadeDTOValidation());

            RuleFor(dto => dto.Cupom)
                .Must(cupom => string.IsNullOrEmpty(cupom) || Enum.IsDefined(typeof(Cupons), cupom))
                .When(cupom => !string.IsNullOrEmpty(cupom.Cupom))
                .WithMessage("Cupom inválido. Por favor, insira um cupom válido do enum Cupons.");
        }
    }
}