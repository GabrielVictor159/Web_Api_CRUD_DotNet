using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using gcsb.ecommerce.tests.Cases.WebApi.UseCases;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace gcsb.ecommerce.tests.Cases.WebApi
{
    public class HttpContextMethods : IHttpContextMethods
    {
         public void SetHttpContextWithClaims<T>(Claim[] claims, T controller) where T : ControllerBase
        {
            var identity = new ClaimsIdentity(claims, "TestAuthType");
            var principal = new ClaimsPrincipal(identity);
            var httpContext = new DefaultHttpContext
            {
                User = principal
            };
            var httpContextAccessor = new HttpContextAccessor { HttpContext = httpContext };
            controller.ControllerContext.HttpContext = httpContextAccessor.HttpContext;
        }
    }
}