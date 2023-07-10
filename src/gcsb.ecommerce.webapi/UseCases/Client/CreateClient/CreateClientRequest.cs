using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace gcsb.ecommerce.webapi.UseCases.Client.CreateClient
{
    public class CreateClientRequest
    {
        public required String Name { get; set; }
        public required String Password { get; set;}
    }
}