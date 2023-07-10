using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using gcsb.ecommerce.application.Boundaries.Client;

namespace gcsb.ecommerce.application.UseCases.Client.CreateClient
{
    public class CreateClientRequest
    {
        public domain.Client.Client Client {get;private set;}
       public CreateClientOutput? CreateClientOutput {get; private set;}
       public CreateClientRequest(domain.Client.Client client)
       {
        Client = client;
       }

       public void SetOutput(Guid id, String Name, String Role)
        =>CreateClientOutput = new CreateClientOutput(id, Name, Role);
    }
}