using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bogus;
using FluentAssertions;
using gcsb.ecommerce.application.Interfaces.Repositories;
using gcsb.ecommerce.application.UseCases.Client.LoginClient;
using gcsb.ecommerce.tests.Builder.Domain.Client;
using Xunit;
using Xunit.Frameworks.Autofac;

namespace gcsb.ecommerce.tests.Cases.Application.Client.LoginClient
{
    [UseAutofacTestFramework]
    public class LoginClientUseCaseTest
    {
        private readonly Faker faker;
        private readonly LoginClientUseCase loginClientUseCase;
        private readonly IClientRepository clientRepository;
        public LoginClientUseCaseTest(
            Faker faker,
            LoginClientUseCase loginClientUseCase,
            IClientRepository clientRepository
        )
        {
            this.faker = faker;
            this.loginClientUseCase = loginClientUseCase;
            this.clientRepository = clientRepository;
        }
        [Fact]
        public async Task ShoulLoginOutputNotBeNullByLoginClientUseCase()
        {
            String Password = faker.Random.String2(8);
            var client = await clientRepository.CreateAsync(ClientBuilder.New(faker).WithPassword(Password).Build());
            var request = new LoginClientRequest(client.Name!, Password);
            await loginClientUseCase.Execute(request);
            request.LoginOutput.Should().NotBeNull();
        }

    }
}