using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace gcsb.ecommerce.webapi.UseCases.Client.GetClients
{
    public class GetClientsRequest
    {
        public String id {get;set;} = "";
        public String Name { get; set; } = "";
        public String Role { get; set; } = "";
        public int page {get; set;} = 1;
        public int pageSize {get; set;} = 10;
    }
}