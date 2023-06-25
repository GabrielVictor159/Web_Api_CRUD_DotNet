using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AutoMapper;
using gcsb.ecommerce.application.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace gcsb.ecommerce.infrastructure.Database.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly Context _context;
        private readonly IMapper _mapper;
        public OrderRepository(Context context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }   
       public async Task<domain.Order.Order> CreateAsync(domain.Order.Order orderDomain)
        {

            var orderEntity = _mapper.Map<Entities.Order>(orderDomain);
            orderEntity.ListOrderProduct = _mapper.Map<List<Entities.OrderProduct>>(orderDomain.ListOrderProduct);
            await _context.Orders.AddAsync(orderEntity);
            await _context.SaveChangesAsync();
            var result = _mapper.Map<domain.Order.Order>(orderEntity);
            result.WithList(_mapper.Map<List<domain.OrderProduct.OrderProduct>>(await getOrderProducts(result.Id)));
            return result;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var entity = await _context.Orders.FirstOrDefaultAsync(o => o.Id == id);
            if(entity==null)
            {
                return false;
            }
            await deleteOrderProducts(id);
            _context.Orders.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<List<domain.Order.Order>> GetOrderAsync(Expression<Func<domain.Order.Order, bool>> func, int page, int pageSize)
        {
           var predicate = _mapper.Map<Expression<Func<Entities.Order, bool>>>(func);
            var query = _context.Orders.Where(predicate);
            var totalItems = await query.CountAsync();
            var totalPages = (int)Math.Ceiling((double)totalItems / pageSize);
            var orders = await query.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();
            var result = _mapper.Map<List<domain.Order.Order>>(orders);
            foreach(var order in result)
            {
                order.WithList(_mapper.Map<List<domain.OrderProduct.OrderProduct>>(await getOrderProducts(order.Id)));
            }
            return result;
        }
        public async Task<domain.Order.Order?> UpdateAsync(domain.Order.Order order)
        {
            var OrderResult = await Task.FromResult(_context.Orders.FirstOrDefault(c => c.Id == order.Id));
            if (OrderResult != null)
            {
            if(await deleteOrderProducts(OrderResult.Id))
            {
            OrderResult = _mapper.Map<Entities.Order>(order);
            OrderResult.ListOrderProduct = _mapper.Map<List<Entities.OrderProduct>>(order.ListOrderProduct);
            await _context.SaveChangesAsync();
            var result = _mapper.Map<domain.Order.Order>(OrderResult);
            return result;
            }
            return null;
            }
            return null;
        }
        private async Task<List<Entities.OrderProduct>> getOrderProducts(Guid id)
        {
            return await _context.OrderProducts.Where(p => p.IdOrder==id).ToListAsync();
        }
        private async Task<Boolean> deleteOrderProducts(Guid id)
        {
            var searchItems = await _context.OrderProducts.Where(p => p.IdOrder==id).ToListAsync();
            if(searchItems.Count==0)
            {
                return false;
            }
            _context.OrderProducts.RemoveRange(searchItems);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}