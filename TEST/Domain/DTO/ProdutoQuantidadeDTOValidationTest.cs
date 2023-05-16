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
    public class ProdutoQuantidadeDTOValidationTest
    {
        private Faker faker = new Faker("pt_BR");

        [Fact]
        public void ProdutoTest()
        {
            ProdutoQuantidadeDTO produtoQuantidadeDTO = new ProdutoQuantidadeDTO()
            { Quantidade = faker.Random.Int(10, 14) };
            ProdutoQuantidadeDTOValidation produtoQuantidadeDTOValidation = new ProdutoQuantidadeDTOValidation();
            var result1 = produtoQuantidadeDTOValidation.Validate(produtoQuantidadeDTO);
            result1.IsValid.Should().BeFalse();
            produtoQuantidadeDTO.Produto = Guid.NewGuid();
            var result2 = produtoQuantidadeDTOValidation.Validate(produtoQuantidadeDTO);
            result2.IsValid.Should().BeTrue();
        }

        [Fact]
        public void QuantidadeTest()
        {
            ProdutoQuantidadeDTO produtoQuantidadeDTO = new ProdutoQuantidadeDTO()
            { Produto = Guid.NewGuid() };
            ProdutoQuantidadeDTOValidation produtoQuantidadeDTOValidation = new ProdutoQuantidadeDTOValidation();
            var result1 = produtoQuantidadeDTOValidation.Validate(produtoQuantidadeDTO);
            result1.IsValid.Should().BeFalse();
            produtoQuantidadeDTO.Quantidade = 0;
            var result2 = produtoQuantidadeDTOValidation.Validate(produtoQuantidadeDTO);
            result2.IsValid.Should().BeFalse();
            produtoQuantidadeDTO.Quantidade = faker.Random.Int(1, 14);
            var result3 = produtoQuantidadeDTOValidation.Validate(produtoQuantidadeDTO);
            result3.IsValid.Should().BeTrue();
        }
    }
}