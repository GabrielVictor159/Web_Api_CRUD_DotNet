using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using gcsb.ecommerce.application.UseCases.Product.UpdateProduct;
using gcsb.ecommerce.domain.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace gcsb.ecommerce.webapi.UseCases.Product.UpdateProducts
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly UpdateProductPresenter presenter;
        private readonly IUpdateProductRequest updateProductRequest;
        public ProductController(
            UpdateProductPresenter createProductPresenter,
            IUpdateProductRequest updateProductRequest)
            {
                this.presenter = createProductPresenter;
                this.updateProductRequest = updateProductRequest;
            }
      [HttpPut]
      [ProducesResponseType(StatusCodes.Status200OK)]
      [ProducesResponseType(StatusCodes.Status404NotFound)]
      [ProducesResponseType(StatusCodes.Status400BadRequest)]
      [ProducesResponseType(StatusCodes.Status500InternalServerError)]
      [Authorize(Roles = nameof(Policies.ADMIN))]
      [Route("UpdateProduct")]
      public async Task<IActionResult> UpdateProduct([FromBody]UpdateProductRequest request)
      { 
        var domain = new domain.Product.Product();
        domain.SetId(request.Id);
        if(request.Name!=null)
        {
            domain.SetName(request.Name);
        }
        if(request.Value != null) 
        {
            domain.SetValue((decimal)request.Value);
        }
        await updateProductRequest.Execute(
            new application.UseCases.Product.UpdateProduct.UpdateProductRequest(domain)
        );
            return presenter.ViewModel;
      }  
    }
}