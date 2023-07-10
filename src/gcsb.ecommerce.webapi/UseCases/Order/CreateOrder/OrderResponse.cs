using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using gcsb.ecommerce.application.Boundaries.Order;

namespace gcsb.ecommerce.webapi.UseCases.Order.CreateOrder
{
    public sealed class OrderResponse
    {
       public CreateOrderOutput? orderOutput {get;set;}
    }
}