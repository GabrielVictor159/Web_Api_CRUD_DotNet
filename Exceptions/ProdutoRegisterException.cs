using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web_Api_CRUD.Exceptions
{
    public class ProdutoRegisterException : Exception
    {
        public ProdutoRegisterException(string mensagem)
       : base(mensagem)
        {
        }


    }
}