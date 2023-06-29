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
         private int _pageSize = 10;
        public int pageSize
        {
            get => _pageSize;
            set => _pageSize = Math.Min(value, 100); 
        }
    }
}