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
using gcsb.ecommerce.webapi.UseCases.Client.UpdateClient;
using Microsoft.AspNetCore.Mvc;
using Xunit;
using Xunit.Frameworks.Autofac;

namespace gcsb.ecommerce.tests.Cases.WebApi.UseCases.Client.UpdateClient
{
    [UseAutofacTestFramework]
    [Collection("ClientController")]
    public class ClientControllerTest
    {
         private readonly Faker faker;
        private readonly ClientController controller;
        private readonly INotificationService notificationService;
        private readonly IHttpContextMethods httpContextMethods;
        private domain.Client.Client? client;
        private string Password = "";
        public ClientControllerTest(
            Faker faker,
            ClientController controller,
            INotificationService notificationService,
            IClientRepository clientRepository,
            IHttpContextMethods httpContextMethods)
        {
            this.faker = faker;
            this.controller = controller;
            this.notificationService = notificationService;
            this.httpContextMethods = httpContextMethods;
             InitializeAsync(clientRepository).Wait();
        }
        private async Task InitializeAsync(IClientRepository clientRepository)
        {
           string password = faker.Random.String2(8);
           Password = password;
            client = await clientRepository.CreateAsync(ClientBuilder.New(faker).WithPassword(password).Build());
        }
        [Fact]
        public async Task ShouldNewAtributesNameBeRequestNewNameByUpdateClient()
        {
            var request = new UpdateClientRequest(){IdUser=client!.Id, newName=faker.Random.String2(8)};
            var claims = new[]
            {
                new Claim("Id",Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.Role,Policies.ADMIN.ToString())
            };
            httpContextMethods.SetHttpContextWithClaims<ClientController>(claims, controller);
            var response = await controller.UpdateClient(request) as OkObjectResult;
            var newAtributes = (response?.Value as dynamic)?.newAtributes as UpdateClientOutput;
            newAtributes!.Name.Should().Be(request.newName);
        }
        [Fact]
        public async Task ShouldNewAtributesBeNullByUpdateClientInvalidOperation()
        {
            var request = new UpdateClientRequest(){IdUser=client!.Id, newName=faker.Random.String2(8)};
            var claims = new[]
            {
                new Claim("Id",Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.Role,Policies.USER.ToString())
            };
            httpContextMethods.SetHttpContextWithClaims<ClientController>(claims, controller);
            var response = await controller.UpdateClient(request) as OkObjectResult;
            var newAtributes = (response?.Value as dynamic)?.newAtributes as UpdateClientOutput;
            newAtributes.Should().BeNull();
        }
    }
}