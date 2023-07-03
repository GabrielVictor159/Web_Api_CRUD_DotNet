using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AutoMapper;
using gcsb.ecommerce.application.Interfaces.Repositories;
using gcsb.ecommerce.application.Interfaces.Services;
using Microsoft.EntityFrameworkCore;

namespace gcsb.ecommerce.infrastructure.Database.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly Context _context;
        private readonly IMapper _mapper;
        private readonly IReflectionMethods _reflectionMethods;
        public OrderRepository(Context context, IMapper mapper, IReflectionMethods reflectionMethods)
        {
            _context = context;
            _mapper = mapper;
            _reflectionMethods = reflectionMethods;
        }   
       public async Task<domain.Order.Order> CreateAsync(domain.Order.Order orderDomain)
        {

            var orderEntity = _mapper.Map<Entities.Order>(orderDomain);
            orderEntity.ListOrderProduct = _mapper.Map<List<Entities.OrderProduct>>(orderDomain.ListOrderProduct);
            await _context.Orders.AddAsync(orderEntity);
            await _context.SaveChangesAsync();
            var result = _mapper.Map<domain.Order.Order>(orderEntity);
            var ListOrderProduct = _mapper.Map<List<domain.OrderProduct.OrderProduct>>(await getOrderProducts(result.Id));
            result.WithList(ListOrderProduct);
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
        public async Task<domain.Order.Order?> GetOrderByIdAsync(Guid id)
        {
             var OrderResult = await Task.FromResult(_context.Orders.FirstOrDefault(c => c.Id == id));
            if (OrderResult != null)
            {
               var OrderResponse = _mapper.Map<domain.Order.Order>(OrderResult);
               OrderResponse.WithList(_mapper.Map<List<domain.OrderProduct.OrderProduct>>(await getOrderProducts(id)));
               return OrderResponse; 
            }
            return null;
        }
        public async Task<domain.Order.Order?> UpdateAsync(domain.Order.Order order)
        {
            var OrderResult = await Task.FromResult(_context.Orders.FirstOrDefault(c => c.Id == order.Id));
            if (OrderResult != null)
            {
            await deleteOrderProducts(OrderResult.Id);
            _reflectionMethods.ReplaceDifferentAttributes(order,OrderResult);
            OrderResult.ListOrderProduct = await saveOrderProducts(order.ListOrderProduct);
            await _context.SaveChangesAsync();
            var result = _mapper.Map<domain.Order.Order>(OrderResult);
            var ListOrderProduct = _mapper.Map<List<domain.OrderProduct.OrderProduct>>(await getOrderProducts(result.Id));
            result.WithList(ListOrderProduct);
            return result;
            }
            return null;
        }
        private async Task<List<Entities.OrderProduct>> saveOrderProducts(List<domain.OrderProduct.OrderProduct> order)
        {
        var list = _mapper.Map<List<Entities.OrderProduct>>(order);
         await _context.OrderProducts.AddRangeAsync(list);
         await _context.SaveChangesAsync();
         return list;
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