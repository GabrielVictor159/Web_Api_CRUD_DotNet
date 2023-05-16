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
    public class ProdutoAtualizarDTOValidationTest
    {
        private Faker faker = new Faker("pt_BR");

        [Fact]
        public void IdTest()
        {
            ProdutoAtualizarDTO produtoAtualizarDTO = new ProdutoAtualizarDTO()
            {
                Nome = faker.Random.String2(10),
                Valor = faker.Random.Decimal(1, 10)
            };
            ProdutoAtualizarDTOValidation produtoAtualizarDTOValidation = new ProdutoAtualizarDTOValidation();
            var result1 = produtoAtualizarDTOValidation.Validate(produtoAtualizarDTO);
            result1.IsValid.Should().BeFalse();
            produtoAtualizarDTO.Id = Guid.NewGuid();
            var result2 = produtoAtualizarDTOValidation.Validate(produtoAtualizarDTO);
            result2.IsValid.Should().BeTrue();
        }
    }
}