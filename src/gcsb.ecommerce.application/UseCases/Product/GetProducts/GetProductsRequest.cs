using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using gcsb.ecommerce.application.Boundaries.Product;

namespace gcsb.ecommerce.application.UseCases.Product.GetProducts
{
    public class GetProductsRequest
    {
        public Expression<Func<domain.Product.Product, bool>> func {get;private set;}
       public int page {get;private set; }
       public int pageSize {get;private set;}
       public List<ProductOutput>? productsResult {get;private set;}
       public GetProductsRequest(Expression<Func<domain.Product.Product,bool>> func, int page, int pageSize)
       {
        this.func = func;
        this.page = page;
        this.pageSize = pageSize;
       }
       public void SetOutput(List<ProductOutput> outputs)
       =>productsResult = outputs;
    }
}