using Microsoft.EntityFrameworkCore.Storage;
using ProductCatalog.Core.Data;
using ProductCatalog.Core.Data.Repositories;
using ProductCatalog.Infrastructure.Data.Repositories;

namespace ProductCatalog.Infrastructure.Data
{
    internal class UnitOfWork : IUnitOfWork
    {
        private DatabaseContext m_DbContext;
        public UnitOfWork(DatabaseContext dbContext)
        {
            m_DbContext = dbContext;
        }

        public IProductBrandRepository ProductCategoryRepository => new ProductBrandRepository(m_DbContext);
        public IProductRepository ProductRepository => new ProductRepository(m_DbContext);
        public IOrderRepository OrderRepository => new OrderRepository(m_DbContext);
        public ICoinRepository CoinRepository => new CoinRepository(m_DbContext);

        public async Task SaveAsync()
        {
            await m_DbContext.SaveChangesAsync();
        }

        public IDbContextTransaction BeginTransaction()
        {
            return m_DbContext.Database.BeginTransaction();
        }
    }
}
