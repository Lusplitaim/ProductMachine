using ProductCatalog.Core.Data;
using ProductCatalog.Core.DTOs.Product;
using ProductCatalog.Core.Exceptions;
using ProductCatalog.Core.Managers;
using ProductCatalog.Core.Models;
using ProductCatalog.Core.Services.Products;
using ProductCatalog.Core.Storages;

namespace ProductCatalog.Core.Services
{
    internal class ProductService : IProductService
    {
        private readonly IProductStorage m_ProductStorage;
        private readonly IUnitOfWork m_UnitOfWork;
        private readonly ILoggerManager m_Logger;
        private readonly IProductBrandStorage m_ProductCategoryStorage;
        public ProductService(IProductStorage productStorage, IUnitOfWork uow, ILoggerManager logger, IProductBrandStorage prodCategoryStorage)
        {
            m_ProductStorage = productStorage;
            m_UnitOfWork = uow;
            m_Logger = logger;
            m_ProductCategoryStorage = prodCategoryStorage;
        }

        public async Task<ICollection<ProductDto>> GetAsync(ProductFilters filters)
        {
            try
            {
                var result = await m_ProductStorage.GetAsync(filters);
                return result;
            }
            catch (Exception ex) when (ex is not RestCoreException)
            {
                m_Logger.LogError(ex, ex.Message);
                throw new Exception("Failed to get products", ex);
            }
        }

        public async Task<ProductDto> GetAsync(int productId)
        {
            try
            {
                var result = await m_ProductStorage.GetAsync(productId);
                return result;
            }
            catch (Exception ex) when (ex is not RestCoreException)
            {
                m_Logger.LogError(ex, ex.Message);
                throw new Exception("Failed to get product", ex);
            }
        }

        public async Task<ProductEditContext> GetContextForEditAsync(int? productId)
        {
            try
            {
                var context = new ProductEditContext();

                if (productId is not null)
                {
                    context.Product = await m_ProductStorage.GetAsync(productId.Value);
                }
                context.Categories = await m_ProductCategoryStorage.GetAsync();

                return context;
            }
            catch (Exception ex) when (ex is not RestCoreException)
            {
                m_Logger.LogError(ex, ex.Message);
                throw new Exception("Failed to get product edit context", ex);
            }
        }

        public async Task<ProductFiltersContext> GetFiltersContextAsync()
        {
            try
            {
                var context = new ProductFiltersContext();

                context.Categories = await m_ProductCategoryStorage.GetAsync();

                return context;
            }
            catch (Exception ex) when (ex is not RestCoreException)
            {
                m_Logger.LogError(ex, ex.Message);
                throw new Exception("Failed to get product filters context", ex);
            }
        }

        public async Task<ExecResult<ProductDto>> CreateAsync(CreateProductDto model)
        {
            try
            {
                using var transaction = m_UnitOfWork.BeginTransaction();

                var result = await m_ProductStorage.CreateAsync(model);

                transaction.Commit();

                return result;
            }
            catch (Exception ex) when (ex is not RestCoreException)
            {
                m_Logger.LogError(ex, ex.Message);
                throw new Exception("Failed to create product", ex);
            }
        }

        public async Task<ExecResult<ProductDto>> UpdateAsync(int productId, UpdateProductDto model)
        {
            try
            {
                using var transaction = m_UnitOfWork.BeginTransaction();

                var result = await m_ProductStorage.UpdateAsync(productId, model);

                transaction.Commit();

                return result;
            }
            catch (Exception ex) when (ex is not RestCoreException)
            {
                m_Logger.LogError(ex, ex.Message);
                throw new Exception("Failed to update product", ex);
            }
        }

        public async Task<ExecResult> DeleteAsync(int productId)
        {
            try
            {
                using var transaction = m_UnitOfWork.BeginTransaction();

                var result = await m_ProductStorage.DeleteAsync(productId);
                
                transaction.Commit();

                return result;
            }
            catch (Exception ex) when (ex is not RestCoreException)
            {
                m_Logger.LogError(ex, ex.Message);
                throw new Exception("Failed to delete product", ex);
            }
        }
    }
}
