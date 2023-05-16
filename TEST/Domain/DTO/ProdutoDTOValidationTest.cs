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
    public class ProdutoDTOValidationTest
    {
        private Faker faker = new Faker("pt_BR");

        [Fact]
        public void NomeTest()
        {
            ProdutoDTO produtoDTO = new ProdutoDTO()
            { Valor = faker.Random.Decimal(10, 14) };
            ProdutoDTOValidation produtoDTOValidation = new ProdutoDTOValidation();
            var result1 = produtoDTOValidation.Validate(produtoDTO);
            result1.IsValid.Should().BeFalse();
            produtoDTO.Nome = "";
            var result2 = produtoDTOValidation.Validate(produtoDTO);
            result2.IsValid.Should().BeFalse();
            produtoDTO.Nome = faker.Random.String2(10);
            var result3 = produtoDTOValidation.Validate(produtoDTO);
            result3.IsValid.Should().BeTrue();
        }

        [Fact]
        public void ValorTest()
        {
            ProdutoDTO produtoDTO = new ProdutoDTO()
            { Nome = faker.Random.String2(10) };
            ProdutoDTOValidation produtoDTOValidation = new ProdutoDTOValidation();
            var result1 = produtoDTOValidation.Validate(produtoDTO);
            result1.IsValid.Should().BeFalse();
            produtoDTO.Valor = 0;
            var result2 = produtoDTOValidation.Validate(produtoDTO);
            result2.IsValid.Should().BeFalse();
            produtoDTO.Valor = faker.Random.Decimal(10, 14);
            var result3 = produtoDTOValidation.Validate(produtoDTO);
            result3.IsValid.Should().BeTrue();
        }
    }
}