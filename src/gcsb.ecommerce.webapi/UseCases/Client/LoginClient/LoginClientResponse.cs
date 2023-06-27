using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace gcsb.ecommerce.webapi.UseCases.Client.LoginClient
{
    public class LoginClientResponse
    {
        public String Token {get; set;}
        public LoginClientResponse(String Token)
        {
            this.Token = Token;
        }
    }
}