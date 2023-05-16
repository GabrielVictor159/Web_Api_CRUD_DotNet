using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using Web_Api_CRUD.Domain.DTO;
using Web_Api_CRUD.Repository;

namespace API.Domain.DTO
{
    public class PedidoProdutoDTOValidation : AbstractValidator<PedidoProdutoDTO>
    {
        public PedidoProdutoDTOValidation()
        {
            RuleFor(dto => dto.PedidoId)
            .NotNull()
            .NotEmpty()
            .WithMessage("O Pedido Produto não pode ter um id de pedido nulo ou vazio.");

            RuleFor(dto => dto.ProdutoId)
            .NotNull()
            .NotEmpty()
            .WithMessage("O Pedido Produto não pode ter um id de produto nulo ou vazio.");

            RuleFor(dto => dto.Quantidade)
            .NotNull()
            .NotEmpty()
            .WithMessage("A quantidade não pode ser nula.")
            .GreaterThan(0)
            .WithMessage("A quantidade deve ser maior que 0.");

        }
    }
}