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
    public class PedidoConsultaDTOValidationTest
    {
        private Faker faker = new Faker("pt_BR");

        [Fact]
        public void IndexTest()
        {
            PedidoConsultaDTO clientePaginationDTO = new PedidoConsultaDTO();
            PedidoConsultaDTOValidation pedidoConsultaDTOValidation = new PedidoConsultaDTOValidation();
            var result1 = pedidoConsultaDTOValidation.Validate(clientePaginationDTO);
            result1.IsValid.Should().BeTrue();
            clientePaginationDTO.index = 0;
            var result2 = pedidoConsultaDTOValidation.Validate(clientePaginationDTO);
            result2.IsValid.Should().BeFalse();
            clientePaginationDTO.index = -1;
            var result3 = pedidoConsultaDTOValidation.Validate(clientePaginationDTO);
            result3.IsValid.Should().BeFalse();
            clientePaginationDTO.index = null;
            var result4 = pedidoConsultaDTOValidation.Validate(clientePaginationDTO);
            result4.IsValid.Should().BeFalse();
        }

        [Fact]
        public void SizeTest()
        {
            PedidoConsultaDTO clientePaginationDTO = new PedidoConsultaDTO();
            PedidoConsultaDTOValidation pedidoConsultaDTOValidation = new PedidoConsultaDTOValidation();
            var result1 = pedidoConsultaDTOValidation.Validate(clientePaginationDTO);
            result1.IsValid.Should().BeTrue();
            clientePaginationDTO.size = 0;
            var result2 = pedidoConsultaDTOValidation.Validate(clientePaginationDTO);
            result2.IsValid.Should().BeFalse();
            clientePaginationDTO.size = -1;
            var result3 = pedidoConsultaDTOValidation.Validate(clientePaginationDTO);
            result3.IsValid.Should().BeFalse();
            clientePaginationDTO.size = null;
            var result4 = pedidoConsultaDTOValidation.Validate(clientePaginationDTO);
            result4.IsValid.Should().BeFalse();
        }
    }
}