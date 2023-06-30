using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using gcsb.ecommerce.application.UseCases.Order.CreateOrder;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace gcsb.ecommerce.webapi.UseCases.Order.CreateOrder
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly CreateOrderPresenter presenter;
        private readonly ICreateOrderRequest createOrderUseCase;
        public OrderController(CreateOrderPresenter presenter, ICreateOrderRequest createOrderUseCase)
        {
            this.presenter = presenter;
            this.createOrderUseCase = createOrderUseCase;
        }
      [HttpPost]
      [ProducesResponseType(StatusCodes.Status200OK)]
      [ProducesResponseType(StatusCodes.Status404NotFound)]
      [ProducesResponseType(StatusCodes.Status400BadRequest)]
      [ProducesResponseType(StatusCodes.Status500InternalServerError)]
      [Authorize]
      [Route("CreateOrder")]
      public async Task<IActionResult> CreateOrder([FromBody]CreateOrderRequest request)  
      {
        var Id = User.FindFirst("Id")?.Value;
        Guid guidId;
        if (Guid.TryParse(Id, out guidId))
        {
             await createOrderUseCase.Execute(new application.UseCases.Order.CreateOrder.CreateOrderRequest(guidId!,request.listProducts,request.Cupons));
        }
        return presenter.ViewModel;
      }
    }
}