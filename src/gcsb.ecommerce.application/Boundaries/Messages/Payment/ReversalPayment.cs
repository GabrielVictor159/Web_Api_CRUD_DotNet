using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace gcsb.ecommerce.application.Boundaries.Messages.Payment
{
    public class ReversalPayment
    {
        public Guid IdPayment {get;set;}
        public decimal ReversedAmount {get;set;}
    }
}