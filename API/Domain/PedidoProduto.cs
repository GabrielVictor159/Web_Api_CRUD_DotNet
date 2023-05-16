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
        public PedidoProduto(int quantidade, Produto produto)
        {
            this.Quantidade = quantidade;
            this.Produto = produto;
            this.IdProduto = produto.Id;
            this.ValorTotalLinha = quantidade * produto.Valor;
        }
        public PedidoProduto()
        {}
        [Required]
        public int Quantidade { get; private set; }

        [Required]
        public decimal ValorTotalLinha { get; private set; }

        public Guid IdPedido { get;  set; }

        [ForeignKey(nameof(IdPedido))]
        [JsonIgnore]
        public Pedido Pedido { get; private set; }

        public Guid IdProduto { get; private set; }

        [ForeignKey(nameof(IdProduto))]
        [JsonIgnore]
        public Produto Produto { get; private set; }
    }
}