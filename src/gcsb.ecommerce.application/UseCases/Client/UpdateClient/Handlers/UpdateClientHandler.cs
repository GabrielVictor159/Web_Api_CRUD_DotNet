using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using gcsb.ecommerce.application.Interfaces.Repositories;
using gcsb.ecommerce.application.Interfaces.Services;
using gcsb.ecommerce.domain.Enums;

namespace gcsb.ecommerce.application.UseCases.Client.UpdateClient.Handlers
{
    public class UpdateClientHandler : Handler<UpdateClientRequest>
    {
        private readonly IClientRepository clientRepository;
        private readonly INotificationService notificationService;
        private readonly IMapper mapper;
        private readonly IReflectionMethods reflectionMethods;
        public UpdateClientHandler(
            IClientRepository clientRepository,
            INotificationService notificationService,
            IMapper mapper,
            IReflectionMethods reflectionMethods
        )
        {
            this.clientRepository = clientRepository;
            this.notificationService = notificationService;
            this.mapper = mapper; 
            this.reflectionMethods = reflectionMethods;
        }
        public override async Task ProcessRequest(UpdateClientRequest request)
        {
            var user = await clientRepository.GetClienteByIdAsync(request.clientUpdate.Id);
            if(user == null){
                notificationService.AddNotification("User not found",$"Unable to find user with id: {request.clientUpdate.Id}.");  
            }
            else
            {
                reflectionMethods.ReplaceDifferentAttributes(request.clientUpdate, user);
                user.ValidateEntity();
                if(!user.IsValid)
                {
                    notificationService.AddNotifications(user.ValidationResult);
                }
                else
                {
                var result = await clientRepository.UpdateAsync(user);
                request.SetOutput(mapper.Map<Boundaries.Client.UpdateClientOutput>(result));
                }
            if(sucessor!=null)
            {
                await sucessor.ProcessRequest(request);
            }
            }  
        }
    }
}