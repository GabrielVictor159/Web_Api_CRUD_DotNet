using gcsb.ecommerce.application.Interfaces.Repositories;

namespace gcsb.ecommerce.application.UseCases.Client.CreateClient.Handlers
{
    public class SaveClientHandler : Handler<CreateClientRequest>
    {
        public readonly IClientRepository clientRepository;
        public SaveClientHandler(IClientRepository clientRepository)
        {
            this.clientRepository = clientRepository;
        }
        public override async Task ProcessRequest(CreateClientRequest request)
        {
            var result = await clientRepository.CreateAsync(request.Client);
            request.SetOutput(result.Id,result.Name!,result.Role!);
        }
    }
}