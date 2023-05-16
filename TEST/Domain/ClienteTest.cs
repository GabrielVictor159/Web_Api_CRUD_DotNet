using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bogus;
using FluentAssertions;
using Web_Api_CRUD.Domain;
using Xunit;

namespace TEST.Domain
{
    public class ClienteTest
    {
        private Faker faker = new Faker("pt_BR");

        [Fact]
        public void CryptographySenhaTest()
        {
            string senha = faker.Random.String2(10);
            Cliente cliente = new Cliente();
            cliente.Senha = senha;
            senha.Should().NotBe(cliente.Senha);
        }
    }
}