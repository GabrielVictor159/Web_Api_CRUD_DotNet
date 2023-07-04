using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace gcsb.ecommerce.application.Boundaries.Messages.Payment
{
    public class ReceivedPayment
    {
        public Guid IdOrder {get;set;}
        public Guid IdPayment {get;set;}
        public decimal AmountPaid {get;set;}
    }
}