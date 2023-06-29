using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using gcsb.ecommerce.application.UseCases.Product.GetProducts;
using Microsoft.AspNetCore.Mvc;

namespace gcsb.ecommerce.webapi.UseCases.Product.GetProducts
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly GetProductsPresenter presenter;
        private readonly GetProductsUseCase getProductsUseCase;
        public ProductController(
            GetProductsPresenter createProductPresenter,
            GetProductsUseCase getProductsUseCase)
            {
                this.presenter = createProductPresenter;
                this.getProductsUseCase = getProductsUseCase;
            }
      [HttpPost]
      [ProducesResponseType(StatusCodes.Status200OK)]
      [ProducesResponseType(StatusCodes.Status404NotFound)]
      [ProducesResponseType(StatusCodes.Status400BadRequest)]
      [ProducesResponseType(StatusCodes.Status500InternalServerError)]
      [Route("GetProducts")]
      public async Task<IActionResult> GetProducts([FromBody]GetProductsRequest request)
      { 
        Expression<Func<domain.Product.Product, bool>> func = p => p.Id.ToString().ToLower().Contains(request.Id.ToLower()) &&
             p.Name.ToLower().Contains(request.Name.ToLower()) &&
             p.Value <= request.MaxValue &&
             p.Value >= request.MinValue;
        await getProductsUseCase.Execute(
            new application.UseCases.Product.GetProducts.GetProductsRequest(
                func,request.Page,request.PageSize
            ));
            return presenter.ViewModel;
      }  
    }
}