using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using gcsb.ecommerce.domain.Order;

namespace gcsb.ecommerce.application.Interfaces.Repositories
{
    public interface IOrderRepository
    {
        Task Add(Order order);
        Task Update(Order order);
        Task<Boolean> Delete(Guid id);
        Task<List<domain.Order.Order>> GetOrder(Expression<Func<domain.Order.Order, bool>> func);
    }
}