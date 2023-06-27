using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bogus;
using FluentAssertions;
using gcsb.ecommerce.application.UseCases.Client.CreateClient;
using gcsb.ecommerce.tests.Builder.Domain.Client;
using Xunit;
using Xunit.Frameworks.Autofac;

namespace gcsb.ecommerce.tests.Cases.Application.Client.CreateClient
{
    [UseAutofacTestFramework]
    public class CreateClientUseCaseTest
    {
         private readonly Faker faker;
         private readonly CreateClientUseCase createClientUseCase;
         public CreateClientUseCaseTest(Faker faker,CreateClientUseCase createClientUseCase)
         {
            this.faker = faker;
            this.createClientUseCase = createClientUseCase;
         }
         [Fact]
         public async Task ShouldNotBeNullOutputByCreateClientUseCase()
         {
            var request = new CreateClientRequest(ClientBuilder.New(faker).Build());
            await createClientUseCase.Execute(request);
            request.CreateClientOutput.Should().NotBeNull();
         }
    }
}