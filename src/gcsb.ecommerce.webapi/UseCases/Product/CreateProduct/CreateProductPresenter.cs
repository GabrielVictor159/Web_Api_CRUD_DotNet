using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using gcsb.ecommerce.application.Boundaries;
using gcsb.ecommerce.application.Boundaries.Product;
using Microsoft.AspNetCore.Mvc;

namespace gcsb.ecommerce.webapi.UseCases.Product.CreateProduct
{
    public class CreateProductPresenter : IOutputPort<CreateProductOutput>
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

        public void Standard(CreateProductOutput output)
        {
            var response = new CreateProductResponse(output);
            this.ViewModel = new OkObjectResult(response);
        }
    
        
    }
}