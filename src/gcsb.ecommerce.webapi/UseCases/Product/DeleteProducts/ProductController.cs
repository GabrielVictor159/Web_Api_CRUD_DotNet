using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using gcsb.ecommerce.application.UseCases.Product.DeleteProduct;
using gcsb.ecommerce.domain.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace gcsb.ecommerce.webapi.UseCases.Product.DeleteProducts
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly DeleteProductPresenter presenter;
        private readonly DeleteProductUseCase createProductUseCase;
        public ProductController(
            DeleteProductPresenter createProductPresenter,
            DeleteProductUseCase createProductUseCase)
            {
                this.presenter = createProductPresenter;
                this.createProductUseCase = createProductUseCase;
            }
      [HttpDelete]
      [ProducesResponseType(StatusCodes.Status200OK)]
      [ProducesResponseType(StatusCodes.Status404NotFound)]
      [ProducesResponseType(StatusCodes.Status400BadRequest)]
      [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    //   [Authorize(Roles = nameof(Policies.ADMIN))]
      [Route("DeleteProduct")]
      public async Task<IActionResult> DeleteProduct([FromBody]DeleteProductRequest request)
      { 
        await createProductUseCase.Execute(
            new application.UseCases.Product.DeleteProduct.DeleteProductRequest(
                request.ProductId
            ));
            return presenter.ViewModel;
      }  
    }
}