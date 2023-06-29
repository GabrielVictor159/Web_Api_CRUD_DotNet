using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using gcsb.ecommerce.application.Boundaries.Product;

namespace gcsb.ecommerce.application.UseCases.Product.DeleteProduct
{
    public class DeleteProductRequest
    {
        public Guid ProductId { get; set; }
        public DeleteProductOutput? deleteOutput { get; set; }
        public DeleteProductRequest(Guid ProductId)
        {
            this.ProductId = ProductId;
        }
       public void SetOutput(Guid id, String Message = "", Boolean Sucess = false)
        =>this.deleteOutput = new DeleteProductOutput(id,Message,Sucess);
    }
}