using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace gcsb.ecommerce.webapi.UseCases.Client.UpdateClient
{
    public class UpdateClientRequest
    {
        public Guid IdUser { get; set; }
        public string? newName { get; set;}
        public string? newPassword {get; set;}
        public string? newRole {get; set;}
    }
}