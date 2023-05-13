using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using Web_Api_CRUD.Domain.DTO;

namespace API.Domain.DTO
{
    public class PedidoConsultaDTOValidation : AbstractValidator<PedidoConsultaDTO>
    {
        public PedidoConsultaDTOValidation()
        {
            RuleFor(dto => dto.index)
            .NotNull()
            .GreaterThanOrEqualTo(1)
            .WithMessage("A propriedade Index deve ser um valor válido do igual ou superior a 1");

            RuleFor(dto => dto.size)
                .NotNull()
                .GreaterThan(0)
                .WithMessage("A propriedade Index deve ser um valor válido superior a 0");
        }
    }
}