using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using gcsb.ecommerce.application.Boundaries;
using gcsb.ecommerce.application.Boundaries.Client;
using gcsb.ecommerce.application.UseCases.Client.UpdateClient.Handlers;

namespace gcsb.ecommerce.application.UseCases.Client.UpdateClient
{
    public class UpdateClientUseCase : IUpdateClientRequest
    {
        private readonly IOutputPort<UpdateClientOutput> outputPort;
        private readonly ValidateClientHandler validateClientHandler;
        public UpdateClientUseCase(
            IOutputPort<UpdateClientOutput> outputPort,
            UpdateClientHandler updateClientHandler,
            ValidateClientHandler validateClientHandler)
        {
            this.outputPort = outputPort;
            this.validateClientHandler = validateClientHandler;
            this.validateClientHandler.SetSucessor(updateClientHandler);
        }

        public async Task Execute(UpdateClientRequest request)
        {
           try{
            await validateClientHandler.ProcessRequest(request);
            outputPort.Standard(request.updateClientOutput!);
            }
            catch(Exception ex)
            {
                outputPort.Error(ex.Message);
            }
        }
    }
}