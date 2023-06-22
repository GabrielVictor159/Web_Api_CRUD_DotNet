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
        public Guid Id { get; private set; }
        public int Amount { get; private set; }
        public decimal TotalOrderLine { get; private set; } = 0;
        public domain.Product.Product Product { get; private set; } = new domain.Product.Product("Initial Product", (decimal)10);
        public Guid IdOrder { get; private set; }
        public Guid IdProduct { get; private set; }

        public OrderProduct(int amount, Guid idOrder, domain.Product.Product product)
        {
            InitializeOrderProduct(Guid.NewGuid(), amount, idOrder, product);
        }

        public OrderProduct(Guid id, int amount, Guid idOrder, domain.Product.Product product)
        {
            InitializeOrderProduct(id, amount, idOrder, product);
        }

        private void InitializeOrderProduct(Guid id, int amount, Guid idOrder, domain.Product.Product product)
        {
            this.Id = id;
            this.Amount = amount;
            this.IdOrder = idOrder;
            this.IdProduct = product.Id;
            this.Product = product;
            this.TotalOrderLine = product.Value * amount;
            Validate(this, new OrderProductValidator());
        }
    }
}
