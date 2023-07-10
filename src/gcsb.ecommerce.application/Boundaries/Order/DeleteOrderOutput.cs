using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace gcsb.ecommerce.application.Boundaries.Order
{
    public class DeleteOrderOutput
    {
        public Guid id {get; private set;}
        public Boolean Sucess {get;private set;}
        public String Message {get; private set;}
        public DeleteOrderOutput(Guid id,  String Message = "",Boolean Sucess = false)
        {
            this.id = id;
            this.Sucess = Sucess;
            this.Message = Message;
        }
    }
}