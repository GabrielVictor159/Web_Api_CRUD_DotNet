using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using gcsb.ecommerce.application.Boundaries;
using gcsb.ecommerce.application.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace gcsb.ecommerce.webapi.UseCases
{
    public abstract class Presenter<Request, Response> : IOutputPort<Request>
    {
        public IActionResult ViewModel { get; private set; } = new ObjectResult(new { StatusCode = 500 });

        public void Error(string message)
        {
            var problemdetails = new ProblemDetails()
            {
                Status = 500,
                Detail = message
            };
            ViewModel = new BadRequestObjectResult(problemdetails);
        }

        public void NotFound(string message)
        {
            ViewModel = new NotFoundObjectResult(message);
        }

        public void Standard(Request request)
        {
            var response = Activator.CreateInstance(typeof(Response), request);
            this.ViewModel = new OkObjectResult(response);
        }
    }
}
