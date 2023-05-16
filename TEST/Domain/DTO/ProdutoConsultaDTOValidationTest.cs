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
    public class ProdutoConsultaDTOValidationTest
    {
        private Faker faker = new Faker("pt_BR");

        [Fact]
        public void IndexTest()
        {
            ProdutoConsultaDTO produtoConsultaDTO = new ProdutoConsultaDTO();
            ProdutoConsultaDTOValidation produtoConsultaDTOValidation = new ProdutoConsultaDTOValidation();
            var result1 = produtoConsultaDTOValidation.Validate(produtoConsultaDTO);
            result1.IsValid.Should().BeTrue();
            produtoConsultaDTO.index = 0;
            var result2 = produtoConsultaDTOValidation.Validate(produtoConsultaDTO);
            result2.IsValid.Should().BeFalse();
            produtoConsultaDTO.index = -1;
            var result3 = produtoConsultaDTOValidation.Validate(produtoConsultaDTO);
            result3.IsValid.Should().BeFalse();
            produtoConsultaDTO.index = null;
            var result4 = produtoConsultaDTOValidation.Validate(produtoConsultaDTO);
            result4.IsValid.Should().BeFalse();
        }

        [Fact]
        public void SizeTest()
        {
            ProdutoConsultaDTO produtoConsultaDTO = new ProdutoConsultaDTO();
            ProdutoConsultaDTOValidation produtoConsultaDTOValidation = new ProdutoConsultaDTOValidation();
            var result1 = produtoConsultaDTOValidation.Validate(produtoConsultaDTO);
            result1.IsValid.Should().BeTrue();
            produtoConsultaDTO.size = 0;
            var result2 = produtoConsultaDTOValidation.Validate(produtoConsultaDTO);
            result2.IsValid.Should().BeFalse();
            produtoConsultaDTO.size = -1;
            var result3 = produtoConsultaDTOValidation.Validate(produtoConsultaDTO);
            result3.IsValid.Should().BeFalse();
            produtoConsultaDTO.size = null;
            var result4 = produtoConsultaDTOValidation.Validate(produtoConsultaDTO);
            result4.IsValid.Should().BeFalse();
        }
    }
}