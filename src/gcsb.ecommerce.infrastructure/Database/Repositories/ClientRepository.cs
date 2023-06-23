using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AutoMapper;
using gcsb.ecommerce.application.Interfaces.Repositories;
using gcsb.ecommerce.domain.Client;
using gcsb.ecommerce.domain.Client.Cryptography;
using Microsoft.EntityFrameworkCore;

namespace gcsb.ecommerce.infrastructure.Database.Repositories
{
    public class ClientRepository : IClientRepository
    {
        private readonly Context _context;
        private readonly IMapper _mapper;
        public ClientRepository(Context context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<domain.Client.Client> CreateAsync(domain.Client.Client clientDomain)
        {
            var client = _mapper.Map<Entities.Client>(clientDomain);
            _context.Clients.Add(client);
            await _context.SaveChangesAsync();
            return _mapper.Map<domain.Client.Client>(client);
        }
        public async Task<List<domain.Client.Client>> GetAllByNameAsync(String Name)
        {
            var list =await _context.Clients.Where(a => a.Name == Name).ToListAsync(); 
            return _mapper.Map<List<domain.Client.Client>>(list);
        }
        public async Task<domain.Client.Client?> Login(String Name, String Password)
        {
            var user = await Task.FromResult(_context.Clients.Where(b => b.Name == Name && b.Password == Cryptography.md5Hash(Password)).FirstOrDefault());
            if(user == null)
            {
                return null;
            }
            return _mapper.Map<domain.Client.Client>(user);
        }
        public async Task<List<domain.Order.Order>> GetAllPagination(Expression<Func<domain.Client.Client, bool>> func, int page, int pageSize)
        {
            var predicate = _mapper.Map<Expression<Func<Entities.Client, bool>>>(func);
            var query = _context.Clients.Where(predicate);
            var totalItems = await query.CountAsync();
            var totalPages = (int)Math.Ceiling((double)totalItems / pageSize);
            var orders = await query.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();
            return _mapper.Map<List<domain.Order.Order>>(orders);
        }
        public async Task<domain.Client.Client?> GetClienteByIdAsync(Guid id)
        {
            var cliente = await Task.FromResult(_context.Clients.FirstOrDefault(c => c.Id == id));
            if (cliente == null)
            {
                return null;
            }
            return _mapper.Map<domain.Client.Client>(cliente);
        }
        public async Task<domain.Client.Client?> UpdateAsync(domain.Client.Client client)
        {
            var clientResult = await Task.FromResult(_context.Clients.FirstOrDefault(c => c.Id == client.Id));
            if (clientResult != null)
            {
                clientResult = _mapper.Map<Entities.Client>(client);
                await _context.SaveChangesAsync();
                return _mapper.Map<domain.Client.Client>(clientResult);
            }
                return null;
        }
    }
}