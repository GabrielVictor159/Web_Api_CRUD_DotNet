using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using gcsb.ecommerce.application.Boundaries.Order;

namespace gcsb.ecommerce.application.UseCases.Order.GetOrder
{

    public class GetOrderRequest
    {
       public Expression<Func<domain.Order.Order, bool>> func {get;private set;}
       public int page {get;private set; }
       public int pageSize {get;private set;}
       public List<OrderOutput>? orderResult {get;private set;}
       public GetOrderRequest(Expression<Func<domain.Order.Order, bool>> func, int page, int pageSize)
       {
        this.func = func;
        this.page = page;
        this.pageSize = pageSize;
       }
       public void SetOutput(List<OrderOutput> orderResult)
        =>this.orderResult = orderResult;
    }
}