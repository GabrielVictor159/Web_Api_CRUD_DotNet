using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Web_Api_CRUD.Model
{
    public class Pedido
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        [Required]
        public decimal ValorTotal { get; set; }
        [Required]
        public List<Guid> Lista { get; set; }
        public PedidoProduto pedidoProduto { get; set; }
    }
}