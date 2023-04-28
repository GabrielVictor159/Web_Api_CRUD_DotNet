using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web_Api_CRUD.Model.DTO
{
    public class PedidoDTO
    {
        public decimal ValorTotal { get; set; }
        public List<Guid> Lista { get; set; }
    }
}