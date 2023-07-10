using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using gcsb.ecommerce.application.UseCases.Order.UpdateOrder;
using gcsb.ecommerce.domain.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace gcsb.ecommerce.webapi.UseCases.Order.UpdateOrder
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderController : ControllerBase
    {
         private readonly UpdateOrderPresenter presenter;
        private readonly IUpdateOrderRequest updateOrderRequest;
        public OrderController(
         UpdateOrderPresenter presenter,
         IUpdateOrderRequest updateOrderRequest)
        {
            this.presenter = presenter;
            this.updateOrderRequest = updateOrderRequest;
        }   
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Authorize(Roles = nameof(Policies.ADMIN))]
        [Route("UpdateOrder")]
         public async Task<IActionResult> UpdateOrder([FromBody]UpdateOrderRequest request)  
      {
       
             await updateOrderRequest.Execute(
                new application.UseCases.Order.UpdateOrder.UpdateOrderRequest()
                {
                    Id=request.Id,
                    IdClient=request.IdClient,
                    Cupons = request.Cupons,
                    IdPayment = request.IdPayment,
                    listProducts = request.listProducts,
                    OrderDate = request.OrderDate,
                });
        return presenter.ViewModel;
      }
    }
}