using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace gcsb.ecommerce.application.Interfaces.Repositories
{
    public interface IProductRepository
    {
        Task<domain.Product.Product> CreateAsync(domain.Product.Product product);
        Task<domain.Product.Product?> UpdateAsync(domain.Product.Product product);
        Task<Boolean> DeleteAsync(Guid id);
        Task<List<domain.Product.Product>> GetOrderAsync(Expression<Func<domain.Product.Product, bool>> func, int page, int pageSize);
    }
}