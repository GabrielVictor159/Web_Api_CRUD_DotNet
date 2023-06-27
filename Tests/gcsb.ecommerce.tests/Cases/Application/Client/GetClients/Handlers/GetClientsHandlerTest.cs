using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Bogus;
using FluentAssertions;
using gcsb.ecommerce.application.Interfaces.Repositories;
using gcsb.ecommerce.application.UseCases.Client.GetClients;
using gcsb.ecommerce.application.UseCases.Client.GetClients.Handlers;
using gcsb.ecommerce.infrastructure.Database;
using gcsb.ecommerce.tests.Builder.Domain.Client;
using Xunit;
using Xunit.Frameworks.Autofac;

namespace gcsb.ecommerce.tests.Cases.Application.Client.GetClients.Handlers
{
    [UseAutofacTestFramework]
    public class GetClientsHandlerTest
    {
        private readonly Faker faker;
        private readonly GetClientsHandler getClientsHandler;
        private List<domain.Client.Client> clients = new();
        public GetClientsHandlerTest(
            Faker faker, 
            GetClientsHandler getClientsHandler,
            IClientRepository clientRepository)
        {
            this.faker = faker;
            this.getClientsHandler = getClientsHandler;
            for(int i = 0; i<5;i++)
            {
                clients.Add(ClientBuilder.New(faker).Build());
            }
            foreach(var client in clients)
            {
                clientRepository.CreateAsync(client).Wait();
            }
        }
        [Fact]
        public async Task ShouldclientResultNotBeNullByGetClientsHandler()
        {
           Expression<Func<domain.Client.Client, bool>> func = p =>
             p.Name!.Contains("");
            var request = new GetClientsRequest(func,1,10);
            await getClientsHandler.ProcessRequest(request);
            request.clientResult.Should().NotBeNull();
        }
    }
}