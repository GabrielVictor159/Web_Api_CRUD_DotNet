using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AutoMapper;
using gcsb.ecommerce.application.Interfaces.Repositories;
using gcsb.ecommerce.application.Interfaces.Services;
using gcsb.ecommerce.domain.Product;
using Microsoft.EntityFrameworkCore;

namespace gcsb.ecommerce.infrastructure.Database.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly Context _context;
        private readonly IMapper _mapper;
        private readonly IReflectionMethods _reflectionMethods;
        public ProductRepository(Context context, IMapper mapper, IReflectionMethods reflectionMethods)
        {
            _context = context;
            _mapper = mapper;
            _reflectionMethods = reflectionMethods;
        }
        public async Task<domain.Product.Product> CreateAsync(domain.Product.Product product)
        {
            var productMap = _mapper.Map<Entities.Product>(product);
            await _context.Products.AddAsync(productMap);
            await _context.SaveChangesAsync();
            return _mapper.Map<domain.Product.Product>(productMap);
        }

        public async Task<Boolean> DeleteAsync(Guid id)
        {
             var entity = await _context.Products.FirstOrDefaultAsync(o => o.Id == id);
            if(entity==null)
            {
                return false;
            }
            List<Entities.OrderProduct> orderProducts = await _context.OrderProducts.Where(p=>p.IdProduct==id).ToListAsync();
            _context.OrderProducts.RemoveRange(orderProducts);
            _context.Products.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<domain.Product.Product>> GetProductAsync(Expression<Func<domain.Product.Product, bool>> func, int page =1, int pageSize=10)
        {
            var predicate = _mapper.Map<Expression<Func<Entities.Product, bool>>>(func);
            var query = _context.Products.Where(predicate);
            var totalItems = await query.CountAsync();
            var totalPages = (int)Math.Ceiling((double)totalItems / pageSize);
            var products= await query.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();
            return _mapper.Map<List<domain.Product.Product>>(products);
        }
        public async Task<domain.Product.Product?> GetProductByIdAsync(Guid Id)
        {
            var product = await _context.Products.FindAsync(Id);
            if(product==null)
            {
                return null;
            }
            return _mapper.Map<domain.Product.Product>(product);
        }
        public async Task<domain.Product.Product?> UpdateAsync(domain.Product.Product product)
        {
           var productResult = await Task.FromResult(_context.Products.FirstOrDefault(c => c.Id == product.Id));
            if (productResult != null)
            {
                 var newAtributes = _mapper.Map<Entities.Product>(product);
                _reflectionMethods.ReplaceDifferentAttributes(newAtributes,productResult);
                await _context.SaveChangesAsync();
                return _mapper.Map<domain.Product.Product>(productResult);
            }
                return null; 
        }
    }
}