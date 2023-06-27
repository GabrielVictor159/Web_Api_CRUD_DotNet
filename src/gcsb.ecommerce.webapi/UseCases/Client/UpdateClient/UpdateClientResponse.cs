using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using gcsb.ecommerce.application.Boundaries.Client;

namespace gcsb.ecommerce.webapi.UseCases.Client.UpdateClient
{
    public class UpdateClientResponse
    {
        public UpdateClientOutput newAtributes {get; set;}
        public UpdateClientResponse(UpdateClientOutput newAtributes)
        {
            this.newAtributes = newAtributes;
        }
    }
}