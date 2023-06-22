using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AutoMapper;
using gcsb.ecommerce.application.Interfaces.Repositories;
using gcsb.ecommerce.domain.Order;

namespace gcsb.ecommerce.infrastructure.Database.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly IMapper _mapper;
        private readonly Context _context;
        public OrderRepository(IMapper mapper, Context context)
        {
            _mapper = mapper;
            _context = context;
        }
        public async Task Add(Order order)
        {
           Entities.Order newOrder = _mapper.Map<Entities.Order>(order);
           await _context.Orders.AddAsync(newOrder);
           await _context.SaveChangesAsync();
        }
        public async Task<Boolean> Delete(Guid id)
        {
            var order = await _context.Orders.FindAsync(id);
            if(order is not null)
            {
            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();
            return true;
            }
            return false;
        }
        public async Task<List<domain.Order.Order>> GetOrder(Expression<Func<domain.Order.Order, bool>> func)
        {
            var predicate = _mapper.Map<Expression<Func<Entities.Order, bool>>>(func);
            var entity = await Task.FromResult(_context.Orders.Where(predicate).ToList());
            return _mapper.Map<List<domain.Order.Order>>(entity);
        }
        public async Task Update(domain.Order.Order order)
        {
            var entity = _mapper.Map<Entities.Order>(order);
            _context.Orders.Update(entity);
            await _context.SaveChangesAsync();
        }
    }
}