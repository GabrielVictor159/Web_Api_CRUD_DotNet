using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AutoMapper;
using gcsb.ecommerce.application.Interfaces.Repositories;
using gcsb.ecommerce.domain.Product;
using Microsoft.EntityFrameworkCore;

namespace gcsb.ecommerce.infrastructure.Database.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly Context _context;
        private readonly IMapper _mapper;
        public ProductRepository(Context context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
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
            _context.Products.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<domain.Product.Product>> GetOrderAsync(Expression<Func<domain.Product.Product, bool>> func, int page, int pageSize)
        {
            var predicate = _mapper.Map<Expression<Func<Entities.Product, bool>>>(func);
            var query = _context.Products.Where(predicate);
            var totalItems = await query.CountAsync();
            var totalPages = (int)Math.Ceiling((double)totalItems / pageSize);
            var products= await query.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();
            return _mapper.Map<List<domain.Product.Product>>(products);
        }

        public async Task<domain.Product.Product?> UpdateAsync(domain.Product.Product product)
        {
           var productResult = await Task.FromResult(_context.Products.FirstOrDefault(c => c.Id == product.Id));
            if (productResult != null)
            {
                productResult = _mapper.Map<Entities.Product>(product);
                await _context.SaveChangesAsync();
                return _mapper.Map<domain.Product.Product>(productResult);
            }
                return null; 
        }
    }
}