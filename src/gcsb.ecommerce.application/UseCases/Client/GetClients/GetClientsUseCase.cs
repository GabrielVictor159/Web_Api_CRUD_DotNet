using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using gcsb.ecommerce.application.Boundaries;
using gcsb.ecommerce.application.Boundaries.Client;
using gcsb.ecommerce.application.UseCases.Client.GetClients.Handlers;

namespace gcsb.ecommerce.application.UseCases.Client.GetClients
{
    public class GetClientsUseCase : IGetClientsRequest
    {
         private readonly IOutputPort<List<GetClientOutput>> outputPort;
         private readonly GetClientsHandler getClientsHandler;
         public GetClientsUseCase(
            IOutputPort<List<GetClientOutput>> outputPort,
            GetClientsHandler getClientsHandler)
         {
            this.outputPort = outputPort;
            this.getClientsHandler = getClientsHandler;
         }
        public async Task Execute(GetClientsRequest request)
        {
            try{
            await getClientsHandler.ProcessRequest(request);
            outputPort.Standard(request.clientResult!);
            }
            catch(Exception ex)
            {
                outputPort.Error(ex.Message);
            }
        }

    }
}