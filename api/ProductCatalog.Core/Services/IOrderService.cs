using ProductCatalog.Core.DTOs.Order;

namespace ProductCatalog.Core.Services
{
    public interface IOrderService
    {
        Task<ICollection<OrderDto>> GetAsync();
    }
}