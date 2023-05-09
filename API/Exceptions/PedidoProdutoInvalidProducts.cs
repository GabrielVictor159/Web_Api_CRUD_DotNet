using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web_Api_CRUD.Exceptions
{
    public class PedidoProdutoInvalidProducts : Exception
    {
        public PedidoProdutoInvalidProducts(string mensagem)
       : base(mensagem)
        {
        }


    }
}