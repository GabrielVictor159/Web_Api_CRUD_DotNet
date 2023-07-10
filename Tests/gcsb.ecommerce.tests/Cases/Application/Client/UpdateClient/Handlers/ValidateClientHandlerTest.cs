using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bogus;
using FluentAssertions;
using gcsb.ecommerce.application.Interfaces.Repositories;
using gcsb.ecommerce.application.Interfaces.Services;
using gcsb.ecommerce.application.UseCases.Client.UpdateClient;
using gcsb.ecommerce.application.UseCases.Client.UpdateClient.Handlers;
using gcsb.ecommerce.domain.Enums;
using gcsb.ecommerce.tests.Builder.Domain.Client;
using Xunit;
using Xunit.Frameworks.Autofac;

namespace gcsb.ecommerce.tests.Cases.Application.Client.UpdateClient.Handlers
{
    [UseAutofacTestFramework]
    public class ValidateClientHandlerTest
    {
        private readonly Faker faker;
        private readonly ValidateClientHandler validateClientHandler;
        private readonly INotificationService notificationService;
        public ValidateClientHandlerTest(
            Faker faker,
            ValidateClientHandler validateClientHandler,
            INotificationService notificationService)
        {
            this.faker = faker;
            this.validateClientHandler = validateClientHandler;
            this.notificationService = notificationService;
        }
        [Fact]
        public async Task ShouldHasNotificationByValidateClientHandlerDiferentUserPoliceUser()
        {
            var client = ClientBuilder.New(faker).Build();
            var request = new UpdateClientRequest(
                ClientBuilder.New(faker).WithId(client.Id).WithRole(client.Role!).Build(),
                Guid.NewGuid().ToString(),
                Policies.USER.ToString());
            await validateClientHandler.ProcessRequest(request);
            notificationService.HasNotifications.Should().BeTrue();
        }
        [Fact]
        public async Task ShouldHasNotificationByValidateClientHandlerUserPoliceUserAlteredByPolicy()
        {
            var client = ClientBuilder.New(faker).Build();
            var request = new UpdateClientRequest(
                ClientBuilder.New(faker).WithId(client.Id).WithRole(Policies.ADMIN.ToString()).Build(),
                client.Id.ToString(),
                Policies.USER.ToString());
            await validateClientHandler.ProcessRequest(request);
            notificationService.HasNotifications.Should().BeTrue();
        }
        [Fact]
        public async Task shouldHasNotificationByValidateClientHandlerDiferentUserPoliceAdmin()
        {
            var client = ClientBuilder.New(faker).Build();
            var request1 = new UpdateClientRequest(
            ClientBuilder.New(faker).WithId(client.Id).Build(),
            Guid.NewGuid().ToString(),
            Policies.ADMIN.ToString());
            await validateClientHandler.ProcessRequest(request1);
            notificationService.HasNotifications.Should().BeFalse();
        }
        [Fact]
        public async Task shouldHasNotificationByValidateClientHandlerUserAdminAlteredRole()
        {
            var client = ClientBuilder.New(faker).Build();
            var request = new UpdateClientRequest(
            ClientBuilder.New(faker).WithId(client.Id).WithRole(Policies.USER.ToString()).Build(),
            client.Id.ToString(),
            Policies.ADMIN.ToString());
            await validateClientHandler.ProcessRequest(request);
            notificationService.HasNotifications.Should().BeFalse();
        }
        [Fact]
        public async Task shouldHasNotificationByValidateClientHandler()
        {
            var client = ClientBuilder.New(faker).Build();
            var request = new UpdateClientRequest(
            ClientBuilder.New(faker).WithId(client.Id).WithRole(Policies.USER.ToString()).Build(),
            client.Id.ToString(),
            Policies.USER.ToString());
            await validateClientHandler.ProcessRequest(request);
            var notifications = notificationService.Notifications;
            notificationService.HasNotifications.Should().BeFalse();
        }
       
    }
}