using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Bogus;
using FluentAssertions;
using gcsb.ecommerce.application.Interfaces.Services;
using gcsb.ecommerce.domain.Enums;
using gcsb.ecommerce.webapi.UseCases.Client.CreateClient;
using Microsoft.AspNetCore.Mvc;
using Xunit;
using Xunit.Frameworks.Autofac;

namespace gcsb.ecommerce.tests.Cases.WebApi.UseCases.Client.CreateClient
{
    [UseAutofacTestFramework]
    [Collection("ClientController")]
    public class ClientControllerTest
    {
        private readonly Faker faker;
        private readonly ClientController controller;
        private readonly INotificationService notificationService;
        public ClientControllerTest(
            Faker faker, 
            ClientController controller,
            INotificationService notificationService)
            {
                this.faker = faker;
                this.controller = controller;
                this.notificationService = notificationService;
            }
        
        [Fact]
        public async Task ShouldHasNotificationBeFalseByCreateClient()
        {
            var request = new CreateClientRequest(){
                Name=faker.Random.String2(8),
                Password=faker.Random.String2(8)};
             await controller.CreateClient(request);
           notificationService.HasNotifications.Should().BeFalse();
        }
        [Fact]
        public async Task ShouldHasNotificationBeTrueByCreateClientInvalidDomain()
        {
            var request = new CreateClientRequest(){
                Name=faker.Random.String2(2),
                Password=faker.Random.String2(8)};
            await controller.CreateClient(request);
            notificationService.HasNotifications.Should().BeTrue();
        }
        [Fact]
        public async Task ShoulHasNotifiactionBeFalseByCreateClientAdmin()
        {
            var request = new CreateClientRequest(){
                Name=faker.Random.String2(8),
                Password=faker.Random.String2(8)};
            await controller.CreateClientAdmin(request);
            notificationService.HasNotifications.Should().BeFalse();
        }
        [Fact]
        public async Task ShoulHasNotifiactionBeTrueByCreateClientAdminInvalidDomain()
        {
            var request = new CreateClientRequest(){
                Name=faker.Random.String2(2),
                Password=faker.Random.String2(8)};
            await controller.CreateClientAdmin(request);
            notificationService.HasNotifications.Should().BeTrue();
        }

    }
}