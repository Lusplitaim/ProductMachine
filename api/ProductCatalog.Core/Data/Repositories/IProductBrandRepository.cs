using ProductCatalog.Core.Data.Entities;

namespace ProductCatalog.Core.Data.Repositories
{
    public interface IProductBrandRepository
    {
        Task<ICollection<ProductBrandEntity>> GetAsync();
        Task<ProductBrandEntity?> GetAsync(int categoryId);
        Task<ProductBrandEntity> CreateAsync(ProductBrandEntity entity);
        void Delete(ProductBrandEntity entity);
    }
}
