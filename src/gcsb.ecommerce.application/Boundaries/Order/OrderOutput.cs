using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace gcsb.ecommerce.application.Boundaries.Order
{
    public class OrderOutput
    {
        public Guid id {get;private set;}
        public OrderOutput(Guid id)
        {
            this.id = id;
        }
    }
}