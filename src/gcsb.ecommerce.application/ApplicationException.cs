using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace gcsb.ecommerce.application
{
    public class ApplicationException : Exception
    {
        public ApplicationException(string businessMessage)
             : base(businessMessage)
        {
        }
    }
}