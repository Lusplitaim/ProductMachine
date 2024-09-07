using ProductCatalog.Core.DTOs.Order;
using ProductCatalog.Core.Models;
using ProductCatalog.Core.Storages.Managers;

namespace ProductCatalog.Core.Storages
{
    public interface IOrderStorage
    {
        Task<ICollection<OrderDto>> GetAsync();
        Task<ExecResult<CreateOrderResult>> CreateAsync(CreateOrderDto model);
    }
}