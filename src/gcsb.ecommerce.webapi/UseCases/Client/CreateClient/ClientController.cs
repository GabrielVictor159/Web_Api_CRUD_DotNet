using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using gcsb.ecommerce.application.UseCases.Client.CreateClient;
using gcsb.ecommerce.domain.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace gcsb.ecommerce.webapi.UseCases.Client.CreateClient
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClientController : ControllerBase
    {
         private readonly CreateClientPresenter presenter;
         private readonly ICreateClientRequest createClientRequest;
         public ClientController(
            CreateClientPresenter presenter,
            ICreateClientRequest createClientRequest)
         {
            this.presenter = presenter;
            this.createClientRequest = createClientRequest;
         }
      
      [HttpPost]
      [ProducesResponseType(StatusCodes.Status200OK)]
      [ProducesResponseType(StatusCodes.Status404NotFound)]
      [ProducesResponseType(StatusCodes.Status400BadRequest)]
      [ProducesResponseType(StatusCodes.Status500InternalServerError)]
      [Route("Register")]
      public async Task<IActionResult> CreateClient([FromBody]CreateClientRequest request)
      { 
        await createClientRequest.Execute(
            new application.UseCases.Client.CreateClient.CreateClientRequest(
               new domain.Client.Client(request.Name,request.Password,Policies.USER.ToString()) 
            ));
            return presenter.ViewModel;
      }  
      [HttpPost]
      [ProducesResponseType(StatusCodes.Status200OK)]
      [ProducesResponseType(StatusCodes.Status404NotFound)]
      [ProducesResponseType(StatusCodes.Status400BadRequest)]
      [ProducesResponseType(StatusCodes.Status500InternalServerError)]
      [Authorize(Roles = nameof(Policies.ADMIN))]
      [Route("RegisterAdmin")]
      public async Task<IActionResult> CreateClientAdmin([FromBody]CreateClientRequest request)
      {
        await createClientRequest.Execute(
            new application.UseCases.Client.CreateClient.CreateClientRequest(
               new domain.Client.Client(request.Name,request.Password,Policies.ADMIN.ToString()) 
            ));
            return presenter.ViewModel;
      }  
    }
}