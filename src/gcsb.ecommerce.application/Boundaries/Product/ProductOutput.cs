using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace gcsb.ecommerce.application.Boundaries.Product
{
     public class ProductOutput
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; } = "";
        public decimal Value { get; private set; } = 0;

        public ProductOutput(Guid id, string name, decimal value)
        {
            Id = id;
            Name = name;
            Value = value;
        }

    }
}