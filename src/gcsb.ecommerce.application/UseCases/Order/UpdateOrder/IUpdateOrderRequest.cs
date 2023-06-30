using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace gcsb.ecommerce.application.UseCases.Order.UpdateOrder
{
    public interface IUpdateOrderRequest
    {
         Task Execute(UpdateOrderRequest request);
    }
}