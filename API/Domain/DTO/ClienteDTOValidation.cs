using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using Web_Api_CRUD.Domain.DTO;
using Web_Api_CRUD.Domain.Enums;

namespace API.Domain.DTO
{
    public class ClienteDTOValidation : AbstractValidator<ClienteDTO>
    {
        public ClienteDTOValidation()
        {
            Include(new LoginDTOValidation());
            RuleFor(clienteDTO => clienteDTO.Role)
                .NotNull()
                .NotEmpty()
                .Must(role => Enum.TryParse(typeof(Policies), role, out _))
                .WithMessage("A propriedade Role deve ser um valor válido do enum Policies.");
        }
    }

}