using Microsoft.AspNetCore.Mvc;
using ProductCatalog.Core.DTOs.ProductCategory;
using ProductCatalog.Core.Extensions;
using ProductCatalog.Core.Services;

namespace ProductCatalog.API.Controllers
{
    public class ProductBrandsController : BaseController
    {
        private readonly IProductBrandService m_ProductBrandService;
        public ProductBrandsController(IProductBrandService pcs)
        {
            m_ProductBrandService = pcs;
        }

        [HttpGet]
        public async Task<IActionResult> GetCategories()
        {
            var result = await m_ProductBrandService.GetAsync();
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateCategory(CreateProductBrandDto model)
        {
            var result = await m_ProductBrandService.CreateAsync(model);
            return this.ResolveResult(result, () => Created(nameof(CreateCategory), result.Result));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            var result = await m_ProductBrandService.DeleteAsync(id);
            return this.ResolveResult(result, Ok);
        }
    }
}
