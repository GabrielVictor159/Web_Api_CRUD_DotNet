using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace gcsb.ecommerce.application.UseCases.Order.GetOrder
{

    public class GetOrderRequest
    {
       public Expression<Func<domain.Order.Order, bool>> func {get;private set;}
       public List<domain.Order.Order>? orderResult {get;private set;}
       public GetOrderRequest(Expression<Func<domain.Order.Order, bool>> func)
       {
        this.func = func;
       }
       public void SetOutput(List<domain.Order.Order> orderResult)
        =>this.orderResult = orderResult;
    }
}