using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web_Api_CRUD.Domain.DTO
{
    public class PedidoConsultaDTO
    {
        public int? index { get; set; } = 1;
        public int? size { get; set; } = 10;
        public Guid? Id { get; set; } = null;
        public Guid? idCliente { get; set; } = null;
        public decimal? valorMinimo { get; set; } = null;
        public decimal? valorMaximo { get; set; } = null;
        public List<ProdutoQuantidadeDTO>? listaProdutos { get; set; } = null;
    }
}