using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web_Api_CRUD.Model.DTO
{
    public class ClienteDTO : LoginDTO
    {
        public string Role { get; set; }
    }
}