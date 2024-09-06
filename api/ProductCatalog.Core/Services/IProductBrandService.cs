using ProductCatalog.Core.DTOs.ProductCategory;
using ProductCatalog.Core.Models;

namespace ProductCatalog.Core.Services
{
    public interface IProductBrandService
    {
        Task<ICollection<ProductBrandDto>> GetAsync();
        Task<ExecResult<ProductBrandDto>> CreateAsync(CreateProductBrandDto model);
        Task<ExecResult> DeleteAsync(int categoryId);
    }
}
