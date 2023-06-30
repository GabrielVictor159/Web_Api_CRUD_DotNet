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
         private readonly IUpdateClientRequest updateClientRequest;
         public ClientController(
            UpdateClientPresenter presenter,
            IUpdateClientRequest updateClientRequest)
         {
            this.presenter = presenter;
            this.updateClientRequest = updateClientRequest;
         }
      [HttpPut]
      [ProducesResponseType(StatusCodes.Status200OK)]
      [ProducesResponseType(StatusCodes.Status404NotFound)]
      [ProducesResponseType(StatusCodes.Status400BadRequest)]
      [ProducesResponseType(StatusCodes.Status500InternalServerError)]
      [Authorize]
      [Route("Update")]
      public async Task<IActionResult> UpdateClient([FromBody]UpdateClientRequest request)
      { 
        var Id = User.FindFirst("Id")?.Value;
        var userRole = User.FindFirst(ClaimTypes.Role)?.Value;
        await updateClientRequest.Execute(
            new application.UseCases.Client.UpdateClient.UpdateClientRequest
            (
                new domain.Client.Client(request.IdUser,request?.newName,request?.newPassword,request?.newRole),
                Id!,
                userRole!
            )
            );
            return presenter.ViewModel;
      }
    }
}