using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace gcsb.ecommerce.application.Interfaces.Repositories
{
    public interface IOrderRepository
    {
        Task<domain.Order.Order> CreateAsync(domain.Order.Order order);
        Task<domain.Order.Order?> UpdateAsync(domain.Order.Order order);
        Task<Boolean> DeleteAsync(Guid id);
        Task<List<domain.Order.Order>> GetOrderAsync(Expression<Func<domain.Order.Order, bool>> func, int page, int pageSize);
    }
}