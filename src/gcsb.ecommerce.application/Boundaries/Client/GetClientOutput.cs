using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace gcsb.ecommerce.application.Boundaries.Client
{
    public class GetClientOutput
    {
       public Guid Id {get; set;}
        public String Name { get; private set; }
        public String Role { get; private set; }
        public GetClientOutput(Guid id, String name, String role)
        {
            Id = id;
            Name = name;
            Role = role;
        } 
    }
}