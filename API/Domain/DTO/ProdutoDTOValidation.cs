using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using Web_Api_CRUD.Domain.DTO;

namespace API.Domain.DTO
{
    public class ProdutoDTOValidation : AbstractValidator<ProdutoDTO>
    {
        public ProdutoDTOValidation()
        {
            RuleFor(dto => dto.Nome).NotNull().WithMessage("A proriedade Nome não pode ser nula");
            RuleFor(dto => dto.Valor)
                .NotNull()
                .GreaterThan(0)
                .WithMessage("A propriedade Valor deve ser um valor válido superior a 0");
        }
    }
}