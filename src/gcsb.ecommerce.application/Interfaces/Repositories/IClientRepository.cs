using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace gcsb.ecommerce.application.Interfaces.Repositories
{
    public interface IClientRepository
    {
        Task<domain.Client.Client> CreateAsync(domain.Client.Client clientDomain);
        Task<List<domain.Client.Client>> GetAllByNameAsync(string name);
        Task<domain.Client.Client?> Login(string name, string password);
        Task<List<domain.Order.Order>> GetAllPagination(Expression<Func<domain.Client.Client, bool>> func, int page, int pageSize);
        Task<domain.Client.Client?> GetClienteByIdAsync(Guid id);
        Task<domain.Client.Client?> UpdateAsync(domain.Client.Client client);
    }
}