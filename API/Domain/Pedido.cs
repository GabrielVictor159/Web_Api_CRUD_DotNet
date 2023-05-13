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
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        [JsonIgnore]
        public Cliente cliente { get; set; }
        [Required]
        public Guid idCliente { get; set; }
        [Required]
        public decimal ValorTotal { get; set; }
        public List<PedidoProduto> Lista { get; set; } = new();
    }
}