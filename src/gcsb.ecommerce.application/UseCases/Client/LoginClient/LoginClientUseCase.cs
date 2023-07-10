using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using gcsb.ecommerce.application.Boundaries;
using gcsb.ecommerce.application.UseCases.Client.LoginClient.Handlers;

namespace gcsb.ecommerce.application.UseCases.Client.LoginClient
{
    public class LoginClientUseCase : ILoginClientRequest
    {
        private readonly IOutputPort<String> outputPort;
        private readonly LoginClientHandler loginClientHandler;
        public LoginClientUseCase(
            IOutputPort<String> outputPort,
            LoginClientHandler loginClientHandler
        )
        {
            this.outputPort = outputPort;
            this.loginClientHandler = loginClientHandler;
        }
        public async Task Execute(LoginClientRequest request)
        {
            try{
            await loginClientHandler.ProcessRequest(request);
            outputPort.Standard(request.LoginOutput!);
            }
            catch(Exception ex)
            {
                outputPort.Error(ex.Message);
            }  
        }
    }
}