using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Web_Api_CRUD.Model
{
    [Index(nameof(Nome), IsUnique = true)]
    public class Produto
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        [Required]
        [StringLength(50)]
        public String Nome { get; set; }
        [Required]
        public decimal Valor { get; set; }
        public List<PedidoProduto> Lista { get; set; } = new();
    }
}