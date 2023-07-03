using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using gcsb.ecommerce.application.UseCases.Order.GetOrder;
using gcsb.ecommerce.domain.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace gcsb.ecommerce.webapi.UseCases.Order.GetOrder
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly GetOrderPresenter presenter;
        private readonly IGetOrderRequest getOrderRequest;
        public OrderController(
         GetOrderPresenter presenter,
         IGetOrderRequest getOrderRequest)
        {
            this.presenter = presenter;
            this.getOrderRequest = getOrderRequest;
        }   
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Authorize(Roles = nameof(Policies.ADMIN))]
        [Route("GetOrder")]
        public async Task<IActionResult> GetOrder([FromBody] GetOrderRequest orderRequest)
        {
             Expression<Func<domain.Order.Order, bool>> func = p => p.Id.ToString().ToLower().Contains(orderRequest.Id!.ToLower()) &&
             p.OrderDate.ToString()!.ToLower().Contains(orderRequest.OrderDate!.ToLower()) &&
             p.IdClient.ToString().ToLower().Contains(orderRequest.IdClient!.ToLower()) &&
             p.IdPayment.ToString().ToLower().Contains(orderRequest.IdPayment!.ToLower()) &&
             p.TotalOrder <= orderRequest.MaximalOrder &&
             p.TotalOrder >= orderRequest.MinimalOrder;
            await getOrderRequest.Execute(new application.UseCases.Order.GetOrder.GetOrderRequest(func, orderRequest.page,orderRequest.pageSize));
            return presenter.ViewModel;
        }
    }

}