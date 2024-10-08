﻿using Microsoft.EntityFrameworkCore;
using ProductCatalog.Core.Data.Entities;
using ProductCatalog.Core.Data.Repositories;
using ProductCatalog.Core.Models;

namespace ProductCatalog.Infrastructure.Data.Repositories
{
    internal class ProductRepository : IProductRepository
    {
        private DatabaseContext m_DbContext;
        public ProductRepository(DatabaseContext dbContext)
        {
            m_DbContext = dbContext;
        }

        public async Task<ICollection<ProductEntity>> GetAsync(ProductFilters filters)
        {
            IQueryable<ProductEntity> products = m_DbContext.Products.Include(p => p.Brand);

            if (filters.Brand is not null)
            {
                products = products.Where(p => p.Brand.Id == filters.Brand);
            }
            if (filters.MaxPrice is not null)
            {
                products = products.Where(p => p.Price <= filters.MaxPrice);
            }

            return await products.ToListAsync();
        }

        public async Task<ICollection<ProductEntity>> GetAsync(IEnumerable<int> ids)
        {
            return await m_DbContext.Products.Where(p => ids.Contains(p.Id))
                .Include(p => p.Brand)
                .ToListAsync();
        }

        public async Task<ProductEntity?> GetAsync(int productId)
        {
            return await m_DbContext.Products.Include(p => p.Brand)
                .SingleOrDefaultAsync(p => p.Id == productId);
        }

        public async Task<ProductEntity> CreateAsync(ProductEntity entity)
        {
            return (await m_DbContext.Products.AddAsync(entity)).Entity;
        }

        public void Delete(ProductEntity entity)
        {
            m_DbContext.Products.Remove(entity);
        }
    }
}
