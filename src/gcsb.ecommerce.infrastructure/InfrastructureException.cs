using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace gcsb.ecommerce.infrastructure
{
    public class InfrastructureException : Exception
    {
        public InfrastructureException(string businessMessage)
             : base(businessMessage)
        {
        }
    
        
    }
}