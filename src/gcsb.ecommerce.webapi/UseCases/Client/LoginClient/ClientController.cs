using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using gcsb.ecommerce.application.UseCases.Client.LoginClient;
using Microsoft.AspNetCore.Mvc;

namespace gcsb.ecommerce.webapi.UseCases.Client.LoginClient
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClientController : ControllerBase
    {
         private readonly LoginClientPresenter presenter;
         private readonly ILoginClientRequest loginClientRequest;
         public ClientController(
            LoginClientPresenter presenter,
            ILoginClientRequest loginClientRequest)
         {
            this.presenter = presenter;
            this.loginClientRequest = loginClientRequest;
         }
      [HttpPost]
      [ProducesResponseType(StatusCodes.Status200OK)]
      [ProducesResponseType(StatusCodes.Status404NotFound)]
      [ProducesResponseType(StatusCodes.Status400BadRequest)]
      [ProducesResponseType(StatusCodes.Status500InternalServerError)]
      [Route("Login")]
      public async Task<IActionResult> Login([FromBody]LoginClientRequest request)
      { 
        await loginClientRequest.Execute(
            new application.UseCases.Client.LoginClient.LoginClientRequest(request.Name,request.Password)
            );
            return presenter.ViewModel;
      }
    }
}