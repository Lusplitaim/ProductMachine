﻿using ProductCatalog.Core.Data;
using ProductCatalog.Core.DTOs.ProductCategory;
using ProductCatalog.Core.Exceptions;
using ProductCatalog.Core.Managers;
using ProductCatalog.Core.Models;
using ProductCatalog.Core.Storages;

namespace ProductCatalog.Core.Services
{
    internal class ProductBrandService : IProductBrandService
    {
        private readonly IProductBrandStorage m_ProductCategoryStorage;
        private readonly IUnitOfWork m_UnitOfWork;
        private readonly ILoggerManager m_Logger;
        public ProductBrandService(IProductBrandStorage pcs, IUnitOfWork uow, ILoggerManager logger)
        {
            m_ProductCategoryStorage = pcs;
            m_UnitOfWork = uow;
            m_Logger = logger;
        }

        public async Task<ExecResult<ProductBrandDto>> CreateAsync(CreateProductBrandDto model)
        {
            try
            {
                using var transaction = m_UnitOfWork.BeginTransaction();

                var result = await m_ProductCategoryStorage.CreateAsync(model);
                
                transaction.Commit();

                return result;
            }
            catch (Exception ex) when (ex is not RestCoreException)
            {
                m_Logger.LogError(ex, ex.Message);
                throw new Exception("Failed to create category", ex);
            }
        }

        public async Task<ExecResult> DeleteAsync(int categoryId)
        {
            try
            {
                using var transaction = m_UnitOfWork.BeginTransaction();

                var result = await m_ProductCategoryStorage.DeleteAsync(categoryId);
                
                transaction.Commit();
                
                return result;
            }
            catch (Exception ex) when (ex is not RestCoreException)
            {
                m_Logger.LogError(ex, ex.Message);
                throw new Exception("Failed to delete category", ex);
            }
        }

        public async Task<ICollection<ProductBrandDto>> GetAsync()
        {
            try
            {
                var result = await m_ProductCategoryStorage.GetAsync();
                return result;
            }
            catch (Exception ex) when (ex is not RestCoreException)
            {
                m_Logger.LogError(ex, ex.Message);
                throw new Exception("Failed to get categories", ex);
            }
        }
    }
}
