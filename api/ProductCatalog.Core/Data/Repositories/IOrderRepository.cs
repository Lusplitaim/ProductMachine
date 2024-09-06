using ProductCatalog.Core.Data.Entities;

namespace ProductCatalog.Core.Data.Repositories
{
    public interface IOrderRepository
    {
        Task<ICollection<OrderEntity>> GetAsync();

        Task<OrderEntity> CreateAsync(OrderEntity entity);
    }
}
