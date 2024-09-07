using Microsoft.EntityFrameworkCore;
using ProductCatalog.Core.Data.Entities;
using ProductCatalog.Core.Data.Repositories;

namespace ProductCatalog.Infrastructure.Data.Repositories
{
    internal class CoinRepository : ICoinRepository
    {
        private DatabaseContext m_DbContext;
        public CoinRepository(DatabaseContext dbContext)
        {
            m_DbContext = dbContext;
        }

        public async Task<ICollection<CoinEntity>> GetAsync()
        {
            return await m_DbContext.Coins.ToListAsync();
        }

        public async Task<ICollection<CoinEntity>> GetAsync(IEnumerable<int> nominals)
        {
            return await m_DbContext.Coins.Where(c => nominals.Contains(c.Nominal))
                .ToListAsync();
        }
    }
}
