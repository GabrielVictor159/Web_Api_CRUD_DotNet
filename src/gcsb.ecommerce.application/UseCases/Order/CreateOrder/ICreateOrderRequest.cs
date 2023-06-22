using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace gcsb.ecommerce.application.UseCases.Order.CreateOrder
{
    public interface ICreateOrderRequest
    {
        Task Execute(CreateOrderRequest request);
    }
}