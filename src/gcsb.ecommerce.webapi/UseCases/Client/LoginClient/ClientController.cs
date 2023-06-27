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
         private readonly LoginClientUseCase loginClientUseCase;
         public ClientController(
            LoginClientPresenter presenter,
            LoginClientUseCase loginClientUseCase)
         {
            this.presenter = presenter;
            this.loginClientUseCase = loginClientUseCase;
         }
      [HttpPost]
      [ProducesResponseType(StatusCodes.Status200OK)]
      [ProducesResponseType(StatusCodes.Status404NotFound)]
      [ProducesResponseType(StatusCodes.Status400BadRequest)]
      [ProducesResponseType(StatusCodes.Status500InternalServerError)]
      [Route("Login")]
      public async Task<IActionResult> Login([FromBody]LoginClientRequest request)
      { 
        await loginClientUseCase.Execute(
            new application.UseCases.Client.LoginClient.LoginClientRequest(request.Name,request.Password)
            );
            return presenter.ViewModel;
      }
    }
}