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
    public class PedidoProdutoDTOValidationTest
    {
        private Faker faker = new Faker("pt_BR");

        [Fact]
        public void PedidoIdTest()
        {
            PedidoProdutoDTO pedidoProdutoDTO = new PedidoProdutoDTO()
            {
                Quantidade = faker.Random.Int(1, 10),
                ProdutoId = Guid.NewGuid()
            };
            PedidoProdutoDTOValidation pedidoProdutoDTOValidation = new PedidoProdutoDTOValidation();
            var result1 = pedidoProdutoDTOValidation.Validate(pedidoProdutoDTO);
            result1.IsValid.Should().BeFalse();
            pedidoProdutoDTO.PedidoId = Guid.NewGuid();
            var result2 = pedidoProdutoDTOValidation.Validate(pedidoProdutoDTO);
            result2.IsValid.Should().BeTrue();
        }

        [Fact]
        public void ProdutoIdTest()
        {
            PedidoProdutoDTO pedidoProdutoDTO = new PedidoProdutoDTO()
            {
                Quantidade = faker.Random.Int(1, 10),
                PedidoId = Guid.NewGuid()
            };
            PedidoProdutoDTOValidation pedidoProdutoDTOValidation = new PedidoProdutoDTOValidation();
            var result1 = pedidoProdutoDTOValidation.Validate(pedidoProdutoDTO);
            result1.IsValid.Should().BeFalse();
            pedidoProdutoDTO.ProdutoId = Guid.NewGuid();
            var result2 = pedidoProdutoDTOValidation.Validate(pedidoProdutoDTO);
            result2.IsValid.Should().BeTrue();
        }

        [Fact]
        public void QuantidadeTest()
        {
            PedidoProdutoDTO pedidoProdutoDTO = new PedidoProdutoDTO()
            {
                ProdutoId = Guid.NewGuid(),
                PedidoId = Guid.NewGuid()
            };
            PedidoProdutoDTOValidation pedidoProdutoDTOValidation = new PedidoProdutoDTOValidation();
            var result1 = pedidoProdutoDTOValidation.Validate(pedidoProdutoDTO);
            result1.IsValid.Should().BeFalse();
            pedidoProdutoDTO.Quantidade = 0;
            var result2 = pedidoProdutoDTOValidation.Validate(pedidoProdutoDTO);
            result2.IsValid.Should().BeFalse();
            pedidoProdutoDTO.Quantidade = faker.Random.Int(1, 10);
            var result3 = pedidoProdutoDTOValidation.Validate(pedidoProdutoDTO);
            result3.IsValid.Should().BeTrue();
        }
    }
}