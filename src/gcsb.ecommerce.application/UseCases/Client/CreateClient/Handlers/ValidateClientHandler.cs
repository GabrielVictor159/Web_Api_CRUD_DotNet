using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using gcsb.ecommerce.application.Interfaces.Services;

namespace gcsb.ecommerce.application.UseCases.Client.CreateClient.Handlers
{
    public class ValidateClientHandler : Handler<CreateClientRequest>
    {
        private readonly INotificationService _notificationService;
        public ValidateClientHandler(INotificationService notificationService)
        {
            _notificationService = notificationService;
        }
        public override async Task ProcessRequest(CreateClientRequest request)
        {
            if(!request.Client.IsValid)
            {
                _notificationService.AddNotifications(request.Client.ValidationResult);
                return;
            }
            if(sucessor!=null)
            {
                await sucessor.ProcessRequest(request);
            }
        }
    }
}