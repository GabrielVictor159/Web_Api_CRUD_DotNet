using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using gcsb.ecommerce.application.UseCases.Product.UpdateProduct;
using Microsoft.AspNetCore.Mvc;

namespace gcsb.ecommerce.webapi.UseCases.Product.UpdateProducts
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly UpdateProductPresenter presenter;
        private readonly UpdateProductUseCase updateProductUseCase;
        public ProductController(
            UpdateProductPresenter createProductPresenter,
            UpdateProductUseCase updateProductUseCase)
            {
                this.presenter = createProductPresenter;
                this.updateProductUseCase = updateProductUseCase;
            }
      [HttpPut]
      [ProducesResponseType(StatusCodes.Status200OK)]
      [ProducesResponseType(StatusCodes.Status404NotFound)]
      [ProducesResponseType(StatusCodes.Status400BadRequest)]
      [ProducesResponseType(StatusCodes.Status500InternalServerError)]
//    [Authorize(Roles = nameof(Policies.ADMIN))]
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
        await updateProductUseCase.Execute(
            new application.UseCases.Product.UpdateProduct.UpdateProductRequest(domain)
        );
            return presenter.ViewModel;
      }  
    }
}