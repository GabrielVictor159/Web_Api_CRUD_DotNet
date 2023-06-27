using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bogus;
using FluentAssertions;
using gcsb.ecommerce.application.Interfaces.Repositories;
using gcsb.ecommerce.application.Interfaces.Services;
using gcsb.ecommerce.tests.Builder.Domain.Client;
using gcsb.ecommerce.webapi.UseCases.Client.LoginClient;
using Microsoft.AspNetCore.Mvc;
using Xunit;
using Xunit.Frameworks.Autofac;

namespace gcsb.ecommerce.tests.Cases.WebApi.UseCases.Client.LoginClient
{
    [UseAutofacTestFramework]
    [Collection("ClientController")]
    public class ClientControllerTest
    {
         private readonly Faker faker;
        private readonly ClientController controller;
        private readonly INotificationService notificationService;
        private domain.Client.Client? client;
        private string Password = "";
        public ClientControllerTest(
            Faker faker,
            ClientController controller,
            INotificationService notificationService,
            IClientRepository clientRepository)
        {
            this.faker = faker;
            this.controller = controller;
            this.notificationService = notificationService;
            
             InitializeAsync(clientRepository).Wait();
        }
        private async Task InitializeAsync(IClientRepository clientRepository)
        {
           string password = faker.Random.String2(8);
           Password = password;
            client = await clientRepository.CreateAsync(ClientBuilder.New(faker).WithPassword(password).Build());
            
        }

        [Fact]
        public async Task ShouldTokenBeOfTypeStringByLogin()
        {
            var request = new LoginClientRequest(){Name=client!.Name!, Password = Password};
            var response = await controller.Login(request) as OkObjectResult;
            var Token = (response?.Value as dynamic)?.Token as string;
            Token.Should().BeOfType<string>();
        }
        [Fact]
        public async Task ShouldTokenBeNullByLoginInvalidUser()
        {
            var request = new LoginClientRequest(){Name=client!.Name!, Password = "dasdas"};
            var response = await controller.Login(request) as OkObjectResult;
            var Token = (response?.Value as dynamic)?.Token as string;
            Token.Should().BeNull();
        }
    }
}