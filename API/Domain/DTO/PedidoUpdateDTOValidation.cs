using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using Web_Api_CRUD.Domain.DTO;

namespace API.Domain.DTO
{
    public class PedidoUpdateDTOValidation : AbstractValidator<PedidoUpdateDTO>
    {
        public PedidoUpdateDTOValidation()
        {
            RuleFor(dto => dto.Id)
            .NotNull()
            .NotEmpty()
            .WithMessage("O Id não pode ser nulo ou vazio.");

            Include(new PedidoDTOValidation());
        }
    }
}