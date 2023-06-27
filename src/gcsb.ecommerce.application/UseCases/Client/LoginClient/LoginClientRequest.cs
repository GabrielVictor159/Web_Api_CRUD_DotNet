using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace gcsb.ecommerce.application.UseCases.Client.LoginClient
{
    public class LoginClientRequest
    {
        public String Name { get; set; }
        public String Password { get; set; }
        public String? LoginOutput {get; set; }
        public LoginClientRequest(String name, String password)
        {
            Name = name;
            Password = password;
        }

         public void SetOutput(String token)
        =>LoginOutput = token;
    }
}