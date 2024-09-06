using ProductCatalog.Core.DTOs.Order;
using ProductCatalog.Core.Models;

namespace ProductCatalog.Core.Storages
{
    public interface IOrderStorage
    {
        Task<ICollection<OrderDto>> GetAsync();
        Task<ExecResult<OrderDto>> CreateAsync(CreateOrderDto model);
    }
}