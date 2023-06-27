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
        private readonly UpdateClientHandler updateClientHandler;
        public UpdateClientUseCase(
            IOutputPort<UpdateClientOutput> outputPort,
            UpdateClientHandler updateClientHandler)
        {
            this.outputPort = outputPort;
            this.updateClientHandler = updateClientHandler;
        }

        public async Task Execute(UpdateClientRequest request)
        {
           try{
            await updateClientHandler.ProcessRequest(request);
            outputPort.Standard(request.updateClientOutput!);
            }
            catch(Exception ex)
            {
                outputPort.Error(ex.Message);
            }
        }
    }
}