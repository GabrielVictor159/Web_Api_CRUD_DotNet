using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web_Api_CRUD.Exceptions
{
    public class ClienteRegisterException : Exception
    {
        public ClienteRegisterException(string mensagem)
       : base(mensagem)
        {
        }
    }
}