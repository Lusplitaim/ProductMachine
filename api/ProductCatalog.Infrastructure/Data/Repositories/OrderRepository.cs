using Microsoft.EntityFrameworkCore;
using ProductCatalog.Core.Data.Entities;
using ProductCatalog.Core.Data.Repositories;

namespace ProductCatalog.Infrastructure.Data.Repositories
{
    internal class OrderRepository : IOrderRepository
    {
        private DatabaseContext m_DbContext;
        public OrderRepository(DatabaseContext dbContext)
        {
            m_DbContext = dbContext;
        }

        public async Task<ICollection<OrderEntity>> GetAsync()
        {
            return await m_DbContext.Orders.ToListAsync();
        }

        public async Task<OrderEntity> CreateAsync(OrderEntity entity)
        {
            return (await m_DbContext.Orders.AddAsync(entity)).Entity;
        }
    }
}
