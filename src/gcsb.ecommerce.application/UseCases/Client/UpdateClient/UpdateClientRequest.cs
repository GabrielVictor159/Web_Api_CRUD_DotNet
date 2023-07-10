using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using gcsb.ecommerce.application.Boundaries.Client;

namespace gcsb.ecommerce.application.UseCases.Client.UpdateClient
{
    public class UpdateClientRequest
    {
        public domain.Client.Client clientUpdate {get;set;}
        public String idUser {get;set;}
        public String Role {get;set;}
        public UpdateClientOutput? updateClientOutput {get;set;}
        public UpdateClientRequest(
            domain.Client.Client clientUpdate,
            String idUser,
            String Role)
            {
                this.clientUpdate = clientUpdate;
                this.idUser = idUser;
                this.Role = Role;
            }
         public void SetOutput(UpdateClientOutput updateClientOutput)
        =>this.updateClientOutput = updateClientOutput;   
    }
}