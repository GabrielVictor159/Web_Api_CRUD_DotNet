using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using gcsb.ecommerce.application.UseCases.Client.UpdateClient;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace gcsb.ecommerce.webapi.UseCases.Client.UpdateClient
{
     [ApiController]
    [Route("api/[controller]")]
    public class ClientController : ControllerBase
    {
         private readonly UpdateClientPresenter presenter;
         private readonly UpdateClientUseCase UpdateClientUseCase;
         public ClientController(
            UpdateClientPresenter presenter,
            UpdateClientUseCase UpdateClientUseCase)
         {
            this.presenter = presenter;
            this.UpdateClientUseCase = UpdateClientUseCase;
         }
      [HttpPost]
      [Authorize]
      [Route("Update")]
      public async Task<IActionResult> Login([FromBody]UpdateClientRequest request)
      { 
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        var userRole = User.FindFirst(ClaimTypes.Role)?.Value;
        await UpdateClientUseCase.Execute(
            new application.UseCases.Client.UpdateClient.UpdateClientRequest
            (
                new domain.Client.Client(request.IdUser,request?.newName,request?.newPassword,request?.newRole),
                userId!,
                userRole!
            )
            );
            return presenter.ViewModel;
      }
    }
}