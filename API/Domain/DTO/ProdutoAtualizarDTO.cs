using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web_Api_CRUD.Domain.DTO
{
    public class ProdutoAtualizarDTO : ProdutoDTO
    {
        public Guid Id { get; set; }
    }
}