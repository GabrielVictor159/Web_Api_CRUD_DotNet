using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Domain.DTO;
using Bogus;
using FluentAssertions;
using Web_Api_CRUD.Domain.DTO;
using Xunit;

namespace TEST.Domain.DTO
{
    public class PedidoUpdateDTOValidationTest
    {
        private Faker faker = new Faker("pt_BR");

        [Fact]
        public void IdTest()
        {
            List<ProdutoQuantidadeDTO> produtoQuantidadeDTOs = new();
            for (int i = 0; i < 10; i++)
            {
                produtoQuantidadeDTOs.Add(new ProdutoQuantidadeDTO() { Produto = Guid.NewGuid(), Quantidade = faker.Random.Int(1, 10) });
            }
            PedidoUpdateDTO pedidoUpdateDTO = new PedidoUpdateDTO()
            {
                listaProdutos = produtoQuantidadeDTOs
            };
            PedidoUpdateDTOValidation pedidoUpdateDTOValidation = new PedidoUpdateDTOValidation();
            var result1 = pedidoUpdateDTOValidation.Validate(pedidoUpdateDTO);
            result1.IsValid.Should().BeFalse();
            pedidoUpdateDTO.Id = Guid.NewGuid();
            var result2 = pedidoUpdateDTOValidation.Validate(pedidoUpdateDTO);
            result2.IsValid.Should().BeTrue();
        }
    }
}