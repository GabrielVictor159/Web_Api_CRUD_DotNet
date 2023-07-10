using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using gcsb.ecommerce.application.Interfaces.Repositories;
using gcsb.ecommerce.application.Interfaces.Services;

namespace gcsb.ecommerce.application.UseCases.Client.LoginClient.Handlers
{
    public class LoginClientHandler : Handler<LoginClientRequest>
    {
        public readonly IClientRepository clientRepository;
        public readonly INotificationService notificationService;
        public readonly ITokenService tokenService;
        public LoginClientHandler(
            IClientRepository clientRepository,
            INotificationService notificationService,
            ITokenService tokenService
        )
        {
            this.clientRepository = clientRepository;
            this.notificationService = notificationService;
            this.tokenService = tokenService;
        }   
        public override async Task ProcessRequest(LoginClientRequest request)
        {
           var result = await clientRepository.Login(request.Name,request.Password);
           if(result==null)
           {
            notificationService.AddNotification("Wrong name or password","Could not find any user with this name and password combination");
           }
           else
           {
            var token = tokenService.GenerateToken(result);
            request.SetOutput(token);
            if(sucessor!=null)
            {
                await sucessor.ProcessRequest(request);
            }
           }
        }
    }
}