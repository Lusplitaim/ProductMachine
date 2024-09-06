using Microsoft.EntityFrameworkCore;
using ProductCatalog.Core.Data.Entities;
using ProductCatalog.Core.Data.Repositories;

namespace ProductCatalog.Infrastructure.Data.Repositories
{
    internal class ProductBrandRepository : IProductBrandRepository
    {
        private DatabaseContext m_DbContext;
        public ProductBrandRepository(DatabaseContext dbContext)
        {
            m_DbContext = dbContext;
        }

        public async Task<ICollection<ProductBrandEntity>> GetAsync()
        {
            return await m_DbContext.ProductBrands.ToListAsync();
        }

        public async Task<ProductBrandEntity?> GetAsync(int categoryId)
        {
            return await m_DbContext.ProductBrands.SingleOrDefaultAsync(pc => pc.Id == categoryId);
        }

        public async Task<ProductBrandEntity> CreateAsync(ProductBrandEntity entity)
        {
            return (await m_DbContext.ProductBrands.AddAsync(entity)).Entity;
        }

        public void Delete(ProductBrandEntity entity)
        {
            m_DbContext.ProductBrands.Remove(entity);
        }
    }
}
