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
    public class LoginDTOValidationTest
    {
        private Faker faker = new Faker("pt_BR");

        [Fact]
        public void NomeTest()
        {
            LoginDTO loginDTO = new LoginDTO()
            {
                Senha = faker.Random.String2(10)
            };
            LoginDTOValidation loginDTOValidation = new LoginDTOValidation();
            var result1 = loginDTOValidation.Validate(loginDTO);
            result1.IsValid.Should().BeFalse();
            loginDTO.Nome = faker.Random.String2(6);
            var result2 = loginDTOValidation.Validate(loginDTO);
            result2.IsValid.Should().BeFalse();
            loginDTO.Nome = faker.Random.String2(10);
            var result3 = loginDTOValidation.Validate(loginDTO);
            result3.IsValid.Should().BeTrue();
        }

        [Fact]
        public void SenhaTest()
        {
            LoginDTO loginDTO = new LoginDTO()
            {
                Nome = faker.Random.String2(10)
            };
            LoginDTOValidation loginDTOValidation = new LoginDTOValidation();
            var result1 = loginDTOValidation.Validate(loginDTO);
            result1.IsValid.Should().BeFalse();
            loginDTO.Senha = faker.Random.String2(6);
            var result2 = loginDTOValidation.Validate(loginDTO);
            result2.IsValid.Should().BeFalse();
            loginDTO.Senha = faker.Random.String2(10);
            var result3 = loginDTOValidation.Validate(loginDTO);
            result3.IsValid.Should().BeTrue();
        }
    }
}