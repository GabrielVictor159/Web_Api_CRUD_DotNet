using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace gcsb.ecommerce.application.UseCases.Client.CreateClient
{
    public interface ICreateClientRequest
    {
        Task Execute(CreateClientRequest request);
    }
}