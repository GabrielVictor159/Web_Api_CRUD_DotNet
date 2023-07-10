using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Bogus;
using FluentAssertions;
using gcsb.ecommerce.application.Boundaries.Client;
using gcsb.ecommerce.application.Interfaces.Repositories;
using gcsb.ecommerce.application.Interfaces.Services;
using gcsb.ecommerce.domain.Enums;
using gcsb.ecommerce.tests.Builder.Domain.Client;
using gcsb.ecommerce.webapi.UseCases.Client.GetClients;
using Microsoft.AspNetCore.Mvc;
using Xunit;
using Xunit.Frameworks.Autofac;

namespace gcsb.ecommerce.tests.Cases.WebApi.UseCases.Client.GetClients
{
    [UseAutofacTestFramework]
    [Collection("ClientController")]
    public class ClientControllerTest
    {
         private readonly Faker faker;
        private readonly ClientController controller;
        private readonly INotificationService notificationService;
        private List<domain.Client.Client> clients = new ();
        public ClientControllerTest(
            Faker faker,
            ClientController controller,
            IHttpContextMethods httpContextMethods,
            INotificationService notificationService,
            IClientRepository clientRepository)
        {
            this.faker = faker;
            this.controller = controller;
            var claims = new[]
            {
                new Claim("Id", Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.Role,Policies.ADMIN.ToString())

            };
             httpContextMethods.SetHttpContextWithClaims(claims,this.controller);
            this.notificationService = notificationService;

            InitializeAsync(clientRepository).Wait();
        }
        private async Task InitializeAsync(IClientRepository clientRepository)
        {
            for (int i = 0; i < 5; i++)
            {
               clients.Add(await clientRepository.CreateAsync(ClientBuilder.New(faker).Build()));
            }
        }
        
        [Fact]
        public async Task ShouldReturnCountBeGreaterThan1ByGetClientsAdminEmptyAttributes()
        {
            var request = new GetClientsRequest();
            var response = await controller.GetClientsAdmin(request) as OkObjectResult;
            var clientsList = (response?.Value as dynamic)?.clients as List<GetClientOutput>;
            clientsList?.Count.Should().BeGreaterThan(1);
        }

        [Fact]
        public async Task ShouldReturnCountBe1GetByClientsAdminEspecificId()
        {
            var request = new GetClientsRequest { id = clients[0].Id.ToString() };
            var response = await controller.GetClientsAdmin(request) as OkObjectResult;
            var clientsList = (response?.Value as dynamic)?.clients as List<GetClientOutput>;
            clientsList?.Count.Should().Be(1);
        }
    }
}