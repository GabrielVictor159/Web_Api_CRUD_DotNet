using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using gcsb.ecommerce.application.Boundaries.Client;

namespace gcsb.ecommerce.application.UseCases.Client.UpdateClient
{
    public interface IUpdateClientRequest
    {
        Task Execute(UpdateClientRequest request);
    }
}