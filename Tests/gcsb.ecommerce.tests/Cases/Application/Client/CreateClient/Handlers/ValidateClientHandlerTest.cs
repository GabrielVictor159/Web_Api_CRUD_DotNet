using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bogus;
using FluentAssertions;
using gcsb.ecommerce.application.Interfaces.Services;
using gcsb.ecommerce.application.UseCases.Client.CreateClient;
using gcsb.ecommerce.application.UseCases.Client.CreateClient.Handlers;
using gcsb.ecommerce.tests.Builder.Domain.Client;
using Xunit;
using Xunit.Frameworks.Autofac;

namespace gcsb.ecommerce.tests.Cases.Application.Client.CreateClient.Handlers
{
    [UseAutofacTestFramework]
    public class ValidateClientHandlerTest
    {
        private readonly Faker faker;
        private readonly ValidateClientHandler ValidateClientHandler;
        private readonly INotificationService notificationService;
        public ValidateClientHandlerTest(
            Faker faker, 
            ValidateClientHandler ValidateClientHandler,
            INotificationService notificationService)
        {
            this.faker = faker;
            this.ValidateClientHandler = ValidateClientHandler;
            this.notificationService = notificationService;
        }
        [Fact]
        public async Task ShouldBeTrueHasNotificationsByValidateClientHandler()
        {
            var request = new CreateClientRequest(ClientBuilder.New(faker).WithName("").Build());
            await ValidateClientHandler.ProcessRequest(request);
            notificationService.HasNotifications.Should().BeTrue();
        }
    }
}