using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace gcsb.ecommerce.tests.Cases.WebApi.UseCases
{
    public interface IHttpContextMethods
    {
        void SetHttpContextWithClaims<T>(Claim[] claims, T controller) where T : ControllerBase;
    }
}