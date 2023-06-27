using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bogus;
using FluentAssertions;
using gcsb.ecommerce.application.Interfaces.Repositories;
using gcsb.ecommerce.application.Interfaces.Services;
using gcsb.ecommerce.application.UseCases.Client.UpdateClient;
using gcsb.ecommerce.domain.Enums;
using gcsb.ecommerce.tests.Builder.Domain.Client;
using Xunit;
using Xunit.Frameworks.Autofac;

namespace gcsb.ecommerce.tests.Cases.Application.Client.UpdateClient
{
    [UseAutofacTestFramework]
    public class UpdateClientUseCaseTest
    {
         private readonly Faker faker;
         private readonly INotificationService notificationService;
         private readonly UpdateClientUseCase updateClientUseCase;
         private readonly IClientRepository clientRepository;
         public UpdateClientUseCaseTest(
            Faker faker, 
            INotificationService notificationService, 
            UpdateClientUseCase updateClientUseCase,
            IClientRepository clientRepository)
            {
                this.faker = faker;
                this.notificationService = notificationService;
                this.updateClientUseCase = updateClientUseCase;
                this.clientRepository = clientRepository;
            }
        
        [Fact]
        public async Task ShouldClientNameBeNewAttributesByUpdateClientUseCaseTest()
        {
            var client = await clientRepository.CreateAsync(ClientBuilder.New(faker).Build());
              var request = new UpdateClientRequest(
                ClientBuilder.New(faker).WithId(client.Id).Build(),
                Guid.NewGuid().ToString(),
                Policies.ADMIN.ToString());
              await updateClientUseCase.Execute(request);
              var newClient = await clientRepository.GetClienteByIdAsync(client.Id);
              newClient!.Name.Should().Be(request.clientUpdate.Name); 
        }
        [Fact]
        public async Task ShouldHasNotificationBeTrueByUpdateClientUseCaseTestDiferentUserPolicieUser()
        {
            var client = await clientRepository.CreateAsync(ClientBuilder.New(faker).Build());
              var request = new UpdateClientRequest(
                ClientBuilder.New(faker).WithId(client.Id).Build(),
                Guid.NewGuid().ToString(),
                Policies.USER.ToString());
              await updateClientUseCase.Execute(request);
              notificationService.HasNotifications.Should().BeTrue();
        }
    }
}