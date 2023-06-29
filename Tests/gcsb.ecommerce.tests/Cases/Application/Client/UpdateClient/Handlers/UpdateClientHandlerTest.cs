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
    public class UpdateClientHandlerTest
    {
         private readonly Faker faker;
         private readonly INotificationService notificationService;
         private readonly UpdateClientHandler updateClientHandler;
         private readonly IClientRepository clientRepository;
         public UpdateClientHandlerTest(
            Faker faker, 
            INotificationService notificationService, 
            UpdateClientHandler updateClientHandler,
            IClientRepository clientRepository)
            {
                this.faker = faker;
                this.notificationService = notificationService;
                this.updateClientHandler = updateClientHandler;
                this.clientRepository = clientRepository;
            }
        
            [Fact]
            public async Task ShouldHasNotificationBeTrueByUpdateClientHandlerInvalidId()
            {
              var client = await clientRepository.CreateAsync(ClientBuilder.New(faker).Build());
              await updateClientHandler.ProcessRequest(
                new UpdateClientRequest(
                ClientBuilder.New(faker).Build(),
                Guid.NewGuid().ToString(),
                Policies.ADMIN.ToString())); 
              notificationService.HasNotifications.Should().BeTrue();
            }
            [Fact]
            public async Task ShouldHasNotificationBeTrueByUpdateClientHandlerInvalidDomain()
            {
              var client = await clientRepository.CreateAsync(ClientBuilder.New(faker).Build());
              await updateClientHandler.ProcessRequest(
                new UpdateClientRequest(
                ClientBuilder.New(faker).WithId(client.Id).WithName("asd").Build(),
                Guid.NewGuid().ToString(),
                Policies.ADMIN.ToString())); 
              notificationService.HasNotifications.Should().BeTrue();
            }
            [Fact]
            public async Task ShouldHasNotificationBeFalseAndOutputNotBeNullByUpdateClientHandler()
            {
              var client = await clientRepository.CreateAsync(ClientBuilder.New(faker).Build());
              var request = new UpdateClientRequest(
                ClientBuilder.New(faker).WithId(client.Id).Build(),
                Guid.NewGuid().ToString(),
                Policies.ADMIN.ToString());
              await updateClientHandler.ProcessRequest(request); 
              notificationService.HasNotifications.Should().BeFalse();
              request.updateClientOutput.Should().NotBeNull();
            }
            
    }
}