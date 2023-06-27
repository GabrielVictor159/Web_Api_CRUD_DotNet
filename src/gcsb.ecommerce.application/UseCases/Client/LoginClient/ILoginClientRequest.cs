using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace gcsb.ecommerce.application.UseCases.Client.LoginClient
{
    public interface ILoginClientRequest
    {
        Task Execute(LoginClientRequest request);
    }
}