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
        private int _pageSize = 10;
        public int pageSize
        {
            get => _pageSize;
            set => _pageSize = Math.Clamp(value, 0, 100);
        }

        private int _page = 1;
        public int page
        {
            get => _page;
            set => _page = Math.Max(value, 1);
        }
    }
}