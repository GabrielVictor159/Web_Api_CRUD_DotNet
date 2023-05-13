using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Web_Api_CRUD.Domain
{
    public class PedidoProduto
    {
        [Required]
        public int Quantidade { get; set; }

        [Required]
        public decimal ValorTotalLinha { get; set; }

        public Guid IdPedido { get; set; }

        [ForeignKey(nameof(IdPedido))]
        [JsonIgnore]
        public Pedido Pedido { get; set; }

        public Guid IdProduto { get; set; }

        [ForeignKey(nameof(IdProduto))]
        [JsonIgnore]
        public Produto Produto { get; set; }
    }
}