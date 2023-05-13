using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web_Api_CRUD.Domain.DTO
{
    public class ClientePaginationDTO
    {
        public string? Id { get; set; } = null;
        public int? Index { get; set; } = 1;
        public int? Size { get; set; } = 10;
        public string? Nome { get; set; } = null;
        public string? Role { get; set; } = null;
    }
}