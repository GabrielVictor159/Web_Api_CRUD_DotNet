using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Web_Api_CRUD.Model
{
    public class Cliente
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        [Required]
        [StringLength(50)]
        public String Nome { get; set; }
        [Required]
        public String Senha
        {
            get
            {
                return Senha;
            }
            set
            {
                Senha = Cryptography.Cryptography.md5Hash(value);
            }
        }
        [Required]
        [StringLength(50)]
        public String Role { get; set; }


    }
}