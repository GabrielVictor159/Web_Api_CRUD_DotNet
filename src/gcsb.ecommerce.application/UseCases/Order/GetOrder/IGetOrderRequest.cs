using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace gcsb.ecommerce.application.UseCases.Order.GetOrder
{
    public interface IGetOrderRequest
    {
        Task Execute(GetOrderRequest request);
    }
}