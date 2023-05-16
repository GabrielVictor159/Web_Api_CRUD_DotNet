using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Web_Api_CRUD.Domain
{
    public class Pedido
    {
        public Pedido(Guid IdCliente,List<PedidoProduto> lista)
        {
            this.idCliente = IdCliente;
            this.Lista = lista;
            decimal valorTotal = 0;
            foreach(PedidoProduto pedidoProduto in lista)
            {
                valorTotal +=pedidoProduto.ValorTotalLinha;
            }
            this.ValorTotal = valorTotal;
        }
        public Pedido()
        {}
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; private set; }
        [JsonIgnore]
        public Cliente cliente { get; private set; }
        [Required]
        public Guid idCliente { get; private set; }
        [Required]
        public decimal ValorTotal { get; private set; }
        public List<PedidoProduto> Lista { get; set ; } = new();

        public void AtualizarLista(List<PedidoProduto> list)
        {
            this.Lista = list;
            decimal valorTotal = 0;
            foreach(PedidoProduto pedidoProduto in list)
            {
                valorTotal +=pedidoProduto.ValorTotalLinha;
            }
            this.ValorTotal = valorTotal;
        }
    }
}