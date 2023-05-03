using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Web_Api_CRUD.Model
{
    [Index(nameof(Nome), IsUnique = true)]
    public class Cliente
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        [Required]
        [StringLength(50)]
        public String Nome { get; set; }
        private String _Senha;
        [Required]
        public String Senha
        {
            get => _Senha;
            set => _Senha = Cryptography.Cryptography.md5Hash(value);
        }
        [Required]
        [StringLength(50)]
        public String Role { get; set; }
        public List<Pedido> pedidos { get; set; }


    }
}