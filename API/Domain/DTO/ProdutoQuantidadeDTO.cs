using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web_Api_CRUD.Domain.DTO
{
    public class ProdutoQuantidadeDTO
    {
        public Guid Produto { get; set; }
        public int Quantidade { get; set; }
    }
}