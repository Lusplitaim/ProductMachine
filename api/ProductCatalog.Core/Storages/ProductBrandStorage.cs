using ProductCatalog.Core.Data;
using ProductCatalog.Core.Data.Entities;
using ProductCatalog.Core.DTOs.ProductCategory;
using ProductCatalog.Core.Exceptions;
using ProductCatalog.Core.Models;

namespace ProductCatalog.Core.Storages
{
    internal class ProductBrandStorage : IProductBrandStorage
    {
        private readonly IUnitOfWork m_UnitOfWork;
        public ProductBrandStorage(IUnitOfWork uow)
        {
            m_UnitOfWork = uow;
        }

        public async Task<ICollection<ProductBrandDto>> GetAsync()
        {
            var entities = await m_UnitOfWork.ProductCategoryRepository.GetAsync();
            return entities.Select(ProductBrandDto.From).ToList();
        }

        public async Task<ExecResult<ProductBrandDto>> CreateAsync(CreateProductBrandDto model)
        {
            var result = new ExecResult<ProductBrandDto>();

            ProductBrandEntity entity = new()
            {
                Name = model.Name.ToLower(),
            };

            await m_UnitOfWork.ProductCategoryRepository.CreateAsync(entity);

            await m_UnitOfWork.SaveAsync();

            result.Result = ProductBrandDto.From(entity);

            return result;
        }

        public async Task<ExecResult> DeleteAsync(int categoryId)
        {
            var result = new ExecResult();

            var entity = await m_UnitOfWork.ProductCategoryRepository.GetAsync(categoryId);
            if (entity is null)
            {
                throw new NotFoundCoreException();
            }

            m_UnitOfWork.ProductCategoryRepository.Delete(entity);

            await m_UnitOfWork.SaveAsync();

            return result;
        }
    }
}
