using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web_Api_CRUD.Model.DTO
{
    public class ClienteResponseDTO
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public string Role { get; set; }
        public List<Pedido> pedidos { get; set; } = new();
    }
}