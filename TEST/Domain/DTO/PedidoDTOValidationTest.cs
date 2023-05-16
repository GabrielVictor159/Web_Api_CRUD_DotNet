using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Domain.DTO;
using API.Domain.Enums;
using Bogus;
using FluentAssertions;
using Web_Api_CRUD.Domain.DTO;
using Xunit;

namespace TEST.Domain.DTO
{
    public class PedidoDTOValidationTest
    {
        private Faker faker = new Faker("pt_BR");

        [Fact]
        public void ListaProdutosTest()
        {
            PedidoDTO pedidoDTO = new PedidoDTO();
            PedidoDTOValidation pedidoDTOValidation = new PedidoDTOValidation();
            var result1 = pedidoDTOValidation.Validate(pedidoDTO);
            result1.IsValid.Should().BeFalse();
            List<ProdutoQuantidadeDTO> produtoQuantidadeDTOs = new();
            for (int i = 0; i < 10; i++)
            {
                produtoQuantidadeDTOs.Add(new ProdutoQuantidadeDTO() { Produto = Guid.NewGuid(), Quantidade = faker.Random.Int(1, 10) });
            }
            pedidoDTO.listaProdutos = produtoQuantidadeDTOs;
            var result2 = pedidoDTOValidation.Validate(pedidoDTO);
            result2.IsValid.Should().BeTrue();
        }

        [Fact]
        public void CupomTest()
        {
            PedidoDTO pedidoDTO = new PedidoDTO();
            PedidoDTOValidation pedidoDTOValidation = new PedidoDTOValidation();
            List<ProdutoQuantidadeDTO> produtoQuantidadeDTOs = new();
            for (int i = 0; i < 10; i++)
            {
                produtoQuantidadeDTOs.Add(new ProdutoQuantidadeDTO() { Produto = Guid.NewGuid(), Quantidade = faker.Random.Int(1, 10) });
            }
            pedidoDTO.listaProdutos = produtoQuantidadeDTOs;
            pedidoDTO.Cupom = faker.Random.String2(10);
            var result1 = pedidoDTOValidation.Validate(pedidoDTO);
            result1.IsValid.Should().BeFalse();
            pedidoDTO.Cupom = Cupons.BlackFriday.ToString();
            var result2 = pedidoDTOValidation.Validate(pedidoDTO);
            result2.IsValid.Should().BeTrue();
        }
    }
}