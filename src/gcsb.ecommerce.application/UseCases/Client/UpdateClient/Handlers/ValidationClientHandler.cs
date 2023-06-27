using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using gcsb.ecommerce.application.Interfaces.Services;
using gcsb.ecommerce.domain.Enums;

namespace gcsb.ecommerce.application.UseCases.Client.UpdateClient.Handlers
{
    public class ValidationClientHandler : Handler<UpdateClientRequest>
    {
        private readonly INotificationService notificationService;
        public ValidationClientHandler(INotificationService notificationService)
        {
            this.notificationService = notificationService;
        }

        public override async Task ProcessRequest(UpdateClientRequest request)
        {
         if(!request.idUser.Equals(request.clientUpdate.Id) && !request.Role.Equals(Policies.ADMIN.ToString())){
                    notificationService.AddNotification("Invalid Request","You are not allowed to make this request.");
            } 
            else if(request.idUser.Equals(request.clientUpdate.Id) && request.Role.Equals(Policies.USER.ToString()) && !request.clientUpdate.Role!.Equals(request.Role))
            {
               notificationService.AddNotification("Invalid Request","You are not allowed to make this request.");  
            }          
            else
            {
                if(sucessor!=null)
                {
                    await sucessor.ProcessRequest(request);
                }
            }   
        }
    }
}