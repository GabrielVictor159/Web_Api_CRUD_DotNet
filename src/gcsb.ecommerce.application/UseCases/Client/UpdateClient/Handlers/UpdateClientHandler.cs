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
        public UpdateClientHandler(
            IClientRepository clientRepository,
            INotificationService notificationService,
            IMapper mapper
        )
        {
            this.clientRepository = clientRepository;
            this.notificationService = notificationService;
            this.mapper = mapper; 
        }
        public override async Task ProcessRequest(UpdateClientRequest request)
        {
            var user = await clientRepository.GetClienteByIdAsync(request.clientUpdate.Id);
            if(user == null){
                notificationService.AddNotification("User not found",$"Unable to find user with id: {request.clientUpdate.Id}.");  
            }
            else
            {
                if(request.clientUpdate.Name!=null)
                {
                    user.WithName(request.clientUpdate.Name);
                }
                if(request.clientUpdate.Password!=null)
                {
                    user.WithPasswordNotCryptography(request.clientUpdate.Password);
                }
                if(request.clientUpdate.Role!=null)
                {
                    user.WithRole(request.clientUpdate.Role);
                }
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