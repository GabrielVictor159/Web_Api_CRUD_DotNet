using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace gcsb.ecommerce.application.Boundaries.Product
{
    public class CreateProductOutput
    {
        public Guid Id { get; private set; }
        public String Name { get;private set; } = "";
        public decimal Value { get;private set; } = 0;

        public CreateProductOutput(Guid id, String name, decimal value)
        {
            Id = id;
            Name = name;
            Value = value;
        }
    }
}