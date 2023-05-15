using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using Web_Api_CRUD.Domain.DTO;

namespace API.Domain.DTO
{
    public class ProdutoConsultaDTOValidation : AbstractValidator<ProdutoConsultaDTO>
    {
        public ProdutoConsultaDTOValidation()
        {
            RuleFor(dto => dto.index)
            .NotNull()
            .NotEmpty()
            .WithMessage("O index não pode ser nulo ou vazio.")
            .GreaterThan(0)
            .WithMessage("O index deve ser maior que 0.");

            RuleFor(dto => dto.size)
            .NotNull()
            .NotEmpty()
            .WithMessage("O size não pode ser nulo ou vazio.")
            .GreaterThan(0)
            .WithMessage("O size deve ser maior que 0.");
        }
    }
}