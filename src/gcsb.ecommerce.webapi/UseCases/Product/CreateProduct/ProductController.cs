using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using gcsb.ecommerce.application.UseCases.Product.CreateProduct;
using gcsb.ecommerce.domain.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace gcsb.ecommerce.webapi.UseCases.Product.CreateProduct
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly CreateProductPresenter presenter;
        private readonly ICreateProductRequest createProductRequest;
        public ProductController(
            CreateProductPresenter createProductPresenter,
            ICreateProductRequest createProductRequest)
            {
                this.presenter = createProductPresenter;
                this.createProductRequest = createProductRequest;
            }
      [HttpPost]
      [ProducesResponseType(StatusCodes.Status200OK)]
      [ProducesResponseType(StatusCodes.Status404NotFound)]
      [ProducesResponseType(StatusCodes.Status400BadRequest)]
      [ProducesResponseType(StatusCodes.Status500InternalServerError)]
      [Authorize(Roles = nameof(Policies.ADMIN))]
      [Route("CreateProduct")]
      public async Task<IActionResult> CreateProduct([FromBody]CreateProductRequest request)
      { 
        await createProductRequest.Execute(
            new application.UseCases.Product.CreateProduct.CreateProductRequest(
                new domain.Product.Product(request.Name,request.Value)
            ));
            return presenter.ViewModel;
      }  
    }
}