using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bogus;
using FluentAssertions;
using gcsb.ecommerce.application.Interfaces.Repositories;
using gcsb.ecommerce.application.Interfaces.Services;
using gcsb.ecommerce.application.UseCases.Client.LoginClient;
using gcsb.ecommerce.application.UseCases.Client.LoginClient.Handlers;
using gcsb.ecommerce.tests.Builder.Domain.Client;
using Xunit;
using Xunit.Frameworks.Autofac;

namespace gcsb.ecommerce.tests.Cases.Application.Client.LoginClient.Handlers
{
    [UseAutofacTestFramework]
    public class LoginClientHandlerTest
    {
        private readonly Faker faker;
        private readonly LoginClientHandler loginClientHandler;
        private readonly INotificationService notificationService;
        private readonly  IClientRepository clientRepository;
        public LoginClientHandlerTest(
            Faker faker,
            LoginClientHandler loginClientHandler,
            INotificationService notificationService,
             IClientRepository clientRepository)
        {
            this.faker = faker;
            this.loginClientHandler = loginClientHandler;
            this.notificationService = notificationService;
            this.clientRepository = clientRepository;
        }
        [Fact]
        public async Task ShouldNotBeNullLoginOutputAndBeFalseHasNotificationByLoginClientHandler()
        {
            String Password = faker.Random.String2(8);
            var client = await clientRepository.CreateAsync(ClientBuilder.New(faker).WithPassword(Password).Build());
            var request = new LoginClientRequest(client.Name!, Password);
            await loginClientHandler.ProcessRequest(request);
            request.LoginOutput.Should().NotBeNull();
            notificationService.HasNotifications.Should().BeFalse();
        }
    }
}