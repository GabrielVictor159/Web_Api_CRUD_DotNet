using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using gcsb.ecommerce.application.UseCases.Client.GetClients;
using gcsb.ecommerce.domain.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace gcsb.ecommerce.webapi.UseCases.Client.GetClients
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClientController : ControllerBase
    {
         private readonly GetClientsPresenter presenter;
         private readonly GetClientsUseCase GetClientsUseCase;
         public ClientController(
            GetClientsPresenter presenter,
            GetClientsUseCase GetClientsUseCase)
         {
            this.presenter = presenter;
            this.GetClientsUseCase = GetClientsUseCase;
         }

      [HttpPost]
      [ProducesResponseType(StatusCodes.Status200OK)]
      [ProducesResponseType(StatusCodes.Status404NotFound)]
      [ProducesResponseType(StatusCodes.Status400BadRequest)]
      [ProducesResponseType(StatusCodes.Status500InternalServerError)]
      [Authorize(Roles = nameof(Policies.ADMIN))]
      [Route("GetClients")]
      public async Task<IActionResult> GetClientsAdmin([FromBody]GetClientsRequest request)
      {
        Expression<Func<domain.Client.Client, bool>> func = p =>
         p.Id.ToString().ToLower().Contains(request.id.ToLower())
         &&
         p.Name!.ToLower().Contains(request.Name.ToLower())
         &&
         p.Role!.ToLower().Contains(request.Role.ToLower());
        await GetClientsUseCase.Execute(
            new application.UseCases.Client.GetClients.GetClientsRequest
            (
                func,request.page,request.pageSize
            ));
            return presenter.ViewModel;
      }  
    }
}