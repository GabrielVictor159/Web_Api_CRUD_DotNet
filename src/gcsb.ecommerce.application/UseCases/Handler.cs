using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace gcsb.ecommerce.application.UseCases
{
    public abstract class Handler<T>
    {
        protected Handler<T>? sucessor;
        public dynamic SetSucessor(Handler<T> sucessor)
        {
            this.sucessor = sucessor;
            return this;
        }
        public abstract Task ProcessRequest(T request);
    }
}