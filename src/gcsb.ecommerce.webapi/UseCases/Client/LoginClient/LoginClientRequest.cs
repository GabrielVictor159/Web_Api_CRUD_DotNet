using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace gcsb.ecommerce.webapi.UseCases.Client.LoginClient
{
    public class LoginClientRequest
    {
        public string Name { get; set;} = "";
        public string Password { get; set;} = "";
    }
}