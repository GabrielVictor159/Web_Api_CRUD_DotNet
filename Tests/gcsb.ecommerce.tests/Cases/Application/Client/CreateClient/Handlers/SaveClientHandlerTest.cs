using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bogus;
using FluentAssertions;
using gcsb.ecommerce.application.UseCases.Client.CreateClient;
using gcsb.ecommerce.application.UseCases.Client.CreateClient.Handlers;
using gcsb.ecommerce.infrastructure.Database;
using gcsb.ecommerce.tests.Builder.Domain.Client;
using Xunit;
using Xunit.Frameworks.Autofac;

namespace gcsb.ecommerce.tests.Cases.Application.Client.CreateClient.Handlers
{
    [UseAutofacTestFramework]
    public class SaveClientHandlerTest
    {
        private readonly Faker faker;
        private readonly SaveClientHandler saveClientHandler;
        private readonly Context context;
        public SaveClientHandlerTest(
            Faker faker, 
            SaveClientHandler saveClientHandler, 
            Context context)
        {
            this.faker = faker;
            this.saveClientHandler = saveClientHandler;
            this.context = context;
        }
        [Fact]
        public async Task ShouldNotBeNullSaveClientBySaveClientHandler()
        {
            var request = new CreateClientRequest(ClientBuilder.New(faker).Build());
            await saveClientHandler.ProcessRequest(request);
            var result = context.Clients.Find(request.Client.Id);
            result.Should().NotBeNull();
        }
    }
}