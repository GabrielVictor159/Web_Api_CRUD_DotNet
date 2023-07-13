using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace gcsb.ecommerce.webapi.UseCases.Client.GetClients
{
    public class GetClientsResponse
    {
        public List<application.Boundaries.Client.GetClientOutput> clients {get; private set;} = new ();

        public GetClientsResponse(List<application.Boundaries.Client.GetClientOutput> clients)
        {
            this.clients = clients;
        }
    }
}