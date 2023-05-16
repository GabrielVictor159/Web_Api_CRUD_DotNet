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
    public class ClientePaginationDTOValidationTest
    {
        private Faker faker = new Faker("pt_BR");

        [Fact]
        public void IndexTest()
        {
            ClientePaginationDTO clientePaginationDTO = new ClientePaginationDTO();
            ClientePaginationDTOValidation clientePaginationDTOValidation = new ClientePaginationDTOValidation();
            var result1 = clientePaginationDTOValidation.Validate(clientePaginationDTO);
            result1.IsValid.Should().BeTrue();
            clientePaginationDTO.Index = 0;
            var result2 = clientePaginationDTOValidation.Validate(clientePaginationDTO);
            result2.IsValid.Should().BeFalse();
            clientePaginationDTO.Index = -1;
            var result3 = clientePaginationDTOValidation.Validate(clientePaginationDTO);
            result3.IsValid.Should().BeFalse();
            clientePaginationDTO.Index = null;
            var result4 = clientePaginationDTOValidation.Validate(clientePaginationDTO);
            result4.IsValid.Should().BeFalse();
        }
        [Fact]
        public void SizeTest()
        {
            ClientePaginationDTO clientePaginationDTO = new ClientePaginationDTO();
            ClientePaginationDTOValidation clientePaginationDTOValidation = new ClientePaginationDTOValidation();
            var result1 = clientePaginationDTOValidation.Validate(clientePaginationDTO);
            result1.IsValid.Should().BeTrue();
            clientePaginationDTO.Size = 0;
            var result2 = clientePaginationDTOValidation.Validate(clientePaginationDTO);
            result2.IsValid.Should().BeFalse();
            clientePaginationDTO.Size = -1;
            var result3 = clientePaginationDTOValidation.Validate(clientePaginationDTO);
            result3.IsValid.Should().BeFalse();
            clientePaginationDTO.Size = null;
            var result4 = clientePaginationDTOValidation.Validate(clientePaginationDTO);
            result4.IsValid.Should().BeFalse();
        }
    }
}