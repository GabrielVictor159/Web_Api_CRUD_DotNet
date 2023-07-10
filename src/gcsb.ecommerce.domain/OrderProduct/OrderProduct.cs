using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using gcsb.ecommerce.domain.Validator.Order;
using gcsb.ecommerce.domain.Validator.OrderProduct;

namespace gcsb.ecommerce.domain.OrderProduct
{
    public class OrderProduct : Entity
    {
        public int Amount { get; private  set; }
        public decimal TotalOrderLine { get; private  set; } = 0;
        public Guid IdOrder { get;  private set; }
        public Guid IdProduct { get; private set; }
        public OrderProduct()
        {}

        public OrderProduct(int amount, Guid idOrder, domain.Product.Product product)
        {
            InitializeOrderProduct( amount, idOrder, product);
        }
        public OrderProduct(int Amount,decimal TotalOrderLine, Guid IdOrder,Guid IdProduct)
        {
            this.Amount = Amount;
            this.IdOrder = IdOrder;
            this.IdProduct = IdProduct;
            this.TotalOrderLine = TotalOrderLine;
            Validate(this, new OrderProductValidator());
        }

        private void InitializeOrderProduct( int amount, Guid idOrder, domain.Product.Product product)
        {
            this.Amount = amount;
            this.IdOrder = idOrder;
            this.IdProduct = product.Id;
            this.TotalOrderLine = (decimal)(product.Value! * amount);
            Validate(this, new OrderProductValidator());
        }
    }
}
