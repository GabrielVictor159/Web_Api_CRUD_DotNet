using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web_Api_CRUD.Domain.DTO
{
    public class PedidoProdutoDTO
    {
        public int Quantidade { get; set; }
        public decimal ValorTotalLinha { get; set; }
        public Guid PedidoId { get; set; }
        public Guid ProdutoId { get; set; }
    }
}