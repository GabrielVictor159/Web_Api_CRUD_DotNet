using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Web_Api_CRUD.Model
{
    public class PedidoProduto
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        [Required]
        public int Quantidade { get; set; }
        [Required]
        public decimal ValorTotalLinha { get; set; }
        [Required]
        public Pedido pedido { get; set; }
        [Required]
        public Produto produto { get; set; }
        [Required]
        public Guid idPedido { get; set; }
        [Required]
        public Guid idProduto { get; set; }
    }
}