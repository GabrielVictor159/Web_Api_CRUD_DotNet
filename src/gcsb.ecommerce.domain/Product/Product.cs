using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using gcsb.ecommerce.domain.Validator.Product;

namespace gcsb.ecommerce.domain.Product
{
    public class Product : Entity
    {
        public Guid Id { get; private set; }
        public String Name { get;private set; } = "";
        public decimal Value { get;private set; }
        public Product()
        {
        }
        public Product(String name, decimal value)
        {
            this.Id = Guid.NewGuid();
            this.Name = name;
            this.Value = value;
            Validate(this,new ProductValidator());
        }
         public Product(Guid id,String name, decimal value)
        {
            this.Id = id;
            this.Name = name;
            this.Value = value;
            Validate(this,new ProductValidator());
        }

    }
}