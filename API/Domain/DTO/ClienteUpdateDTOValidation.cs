using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using Web_Api_CRUD.Domain.DTO;

namespace API.Domain.DTO
{
    public class ClienteUpdateDTOValidation : AbstractValidator<ClienteUpdateDTO>
    {
        public ClienteUpdateDTOValidation()
        {
            Include(new ClienteDTOValidation());
            RuleFor(dto => dto.Id).NotNull().WithMessage("A propriedade Id não pode ser nula");
        }
    }
}