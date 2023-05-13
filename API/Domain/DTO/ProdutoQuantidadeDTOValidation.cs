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
            RuleFor(dto => dto.Produto).NotNull().WithMessage("A propriedade Produto não pode ser nula");
            RuleFor(dto => dto.Quantidade).NotNull().WithMessage("A propriedade Quantidade não pode ser nula");
        }
    }
}