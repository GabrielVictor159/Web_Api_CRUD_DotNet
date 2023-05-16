using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Domain.DTO;
using Bogus;
using FluentAssertions;
using Web_Api_CRUD.Domain.DTO;
using Web_Api_CRUD.Domain.Enums;
using Xunit;

namespace TEST.Domain.DTO
{
    public class ClienteDTOValidationTest
    {
        private Faker faker = new Faker("pt_BR");
        [Fact]
        public void RuleTest()
        {
            ClienteDTO clienteDTO = new ClienteDTO() { Role = Policies.ADMIN.ToString(), Nome = faker.Name.FullName(), Senha = faker.Random.String2(10, 15) };
            var result1 = new ClienteDTOValidation().Validate(clienteDTO);
            result1.IsValid.Should().BeTrue();
            clienteDTO.Role = faker.Random.String2(10);
            var result2 = new ClienteDTOValidation().Validate(clienteDTO);
            result2.IsValid.Should().BeFalse();
            clienteDTO.Role = "";
            var result3 = new ClienteDTOValidation().Validate(clienteDTO);
            result3.IsValid.Should().BeFalse();
            clienteDTO.Role = null;
            var result4 = new ClienteDTOValidation().Validate(clienteDTO);
            result4.IsValid.Should().BeFalse();
        }
    }
}