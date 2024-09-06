﻿using ProductCatalog.Core.Data;
using ProductCatalog.Core.Data.Entities;
using ProductCatalog.Core.DTOs.Product;
using ProductCatalog.Core.Exceptions;
using ProductCatalog.Core.Models;

namespace ProductCatalog.Core.Storages
{
    internal class ProductStorage : IProductStorage
    {
        private readonly IUnitOfWork m_UnitOfWork;
        public ProductStorage(IUnitOfWork uow)
        {
            m_UnitOfWork = uow;
        }

        public async Task<ICollection<ProductDto>> GetAsync(ProductFilters filters)
        {
            var entities = await m_UnitOfWork.ProductRepository.GetAsync(filters);

            return entities.Select(ProductDto.From).ToList();
        }

        public async Task<ProductDto> GetAsync(int productId)
        {
            var entity = await m_UnitOfWork.ProductRepository.GetAsync(productId);
            if (entity is null)
            {
                throw new NotFoundCoreException();
            }

            return ProductDto.From(entity);
        }

        public async Task<ExecResult<ProductDto>> CreateAsync(CreateProductDto model)
        {
            var result = new ExecResult<ProductDto>();

            ProductEntity entity = new()
            {
                Name = model.Name,
                Price = model.Price,
            };

            await m_UnitOfWork.ProductRepository.CreateAsync(entity);

            await m_UnitOfWork.SaveAsync();

            var updated = await GetAsync(entity.Id);
            result.Result = updated;

            return result;
        }

        public async Task<ExecResult<ProductDto>> UpdateAsync(int productId, UpdateProductDto model)
        {
            var result = new ExecResult<ProductDto>();

            var entity = await m_UnitOfWork.ProductRepository.GetAsync(productId);
            if (entity is null)
            {
                throw new NotFoundCoreException();
            }

            entity.Name = model.Name;
            entity.Price = model.Price;
            entity.BrandId = model.BrandId;

            await m_UnitOfWork.SaveAsync();

            var updated = await GetAsync(entity.Id);
            result.Result = updated;

            return result;
        }

        public async Task<ExecResult> DeleteAsync(int productId)
        {
            var result = new ExecResult();

            var entity = await m_UnitOfWork.ProductRepository.GetAsync(productId);
            if (entity is null)
            {
                throw new NotFoundCoreException();
            }

            m_UnitOfWork.ProductRepository.Delete(entity);

            await m_UnitOfWork.SaveAsync();

            return result;
        }
    }
}
