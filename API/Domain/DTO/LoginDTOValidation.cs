using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using Web_Api_CRUD.Domain.DTO;

namespace API.Domain.DTO
{
    public class LoginDTOValidation : AbstractValidator<LoginDTO>
    {
        public LoginDTOValidation()
        {
            RuleFor(loginDTO => loginDTO.Nome).NotNull().MinimumLength(8).WithMessage("A propriedade Nome deve ter pelo menos 8 digitos");
            RuleFor(loginDTO => loginDTO.Senha).NotNull().MinimumLength(8).WithMessage("A propriedade Senha deve ter pelo menos 8 digitos");
        }
    }
}