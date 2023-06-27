using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using gcsb.ecommerce.application.Boundaries.Client;

namespace gcsb.ecommerce.application.UseCases.Client.GetClients
{
    public interface IGetClientsRequest
    {
        Task Execute(GetClientsRequest request);
    }
}