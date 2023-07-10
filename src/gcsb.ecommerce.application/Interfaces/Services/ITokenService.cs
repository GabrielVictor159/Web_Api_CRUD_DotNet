using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using gcsb.ecommerce.domain.Client;

namespace gcsb.ecommerce.application.Interfaces.Services
{
    public interface ITokenService
    {
        string GenerateToken(Client client);
    }
}