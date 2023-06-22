using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace gcsb.ecommerce.application.UseCases.Order.RemoveOrder
{
    public interface IRemoveOrderRequest
    {
        Task Execute(RemoveOrderRequest request);
    }
}