using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using gcsb.ecommerce.application.Interfaces.Repositories;

namespace gcsb.ecommerce.application.UseCases.Client.GetClients.Handlers
{
    public class GetClientsHandler : Handler<GetClientsRequest>
    {
        private readonly IClientRepository clientRepository;
        private readonly IMapper mapper;
        public GetClientsHandler(
            IClientRepository clientRepository,
            IMapper mapper)
        {
            this.clientRepository = clientRepository;
            this.mapper = mapper;
        }
        public override async Task ProcessRequest(GetClientsRequest request)
        {
            var result = await clientRepository.GetAllPagination(request.func,request.page,request.pageSize);
            request.SetOutput(mapper.Map<List<Boundaries.Client.GetClientOutput>>(result));
            if(sucessor!=null)
            {
                await sucessor.ProcessRequest(request);
            }
        }
    }
}