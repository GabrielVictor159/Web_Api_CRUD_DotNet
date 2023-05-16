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
    public class ClienteUpdateDTOValidationTest
    {
        private Faker faker = new Faker("pt_BR");

        [Fact]
        public void IdTest()
        {
            ClienteUpdateDTO clienteUpdateDTO = new ClienteUpdateDTO()
            {
                Id = Guid.NewGuid(),
                Role = Policies.ADMIN.ToString(),
                Nome = faker.Random.String2(10, 15),
                Senha = faker.Random.String2(10, 15)
            };
            ClienteUpdateDTOValidation clienteUpdateDTOValidation = new ClienteUpdateDTOValidation();
            var result1 = clienteUpdateDTOValidation.Validate(clienteUpdateDTO);
            result1.IsValid.Should().BeTrue();
        }
    }
}