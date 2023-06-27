using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using gcsb.ecommerce.application.Boundaries;
using gcsb.ecommerce.application.Boundaries.Client;
using gcsb.ecommerce.application.UseCases.Client.CreateClient.Handlers;

namespace gcsb.ecommerce.application.UseCases.Client.CreateClient
{
    public class CreateClientUseCase : ICreateClientRequest
    {
        private readonly IOutputPort<CreateClientOutput> outputPort;
        private readonly ValidateClientHandler validateClientHandler;
        public CreateClientUseCase(
            IOutputPort<CreateClientOutput> outputPort,
            ValidateClientHandler validateClientHandler,
            SaveClientHandler saveClientHandler
            )
        {
            this.outputPort = outputPort;
            this.validateClientHandler = validateClientHandler;
            this.validateClientHandler.SetSucessor(saveClientHandler);
        }
        public async Task Execute(CreateClientRequest request)
        {
            try
            {
            await validateClientHandler.ProcessRequest(request);
            outputPort.Standard(
                new CreateClientOutput(
                    request.Client.Id,
                    request.Client.Name!,
                    request.Client.Role!
                    ));
            }
            catch (Exception ex)
            {
                outputPort.Error(ex.Message);
            }
        }
    }
}