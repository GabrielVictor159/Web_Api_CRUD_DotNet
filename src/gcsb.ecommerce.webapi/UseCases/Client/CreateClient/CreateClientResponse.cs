using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using gcsb.ecommerce.application.Boundaries.Client;

namespace gcsb.ecommerce.webapi.UseCases.Client.CreateClient
{
    public class CreateClientResponse
    {
        public Guid Id {get; set;}
        public String Name { get;  set; }
        public String Role { get;  set; }
        public CreateClientResponse(CreateClientOutput output)
        {
            Id = output.Id;
            Name = output.Name;
            Role = output.Role;
        }
       
    }
}