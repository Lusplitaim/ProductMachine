using ProductCatalog.Core.DTOs.Coin;
using ProductCatalog.Core.DTOs.Order;
using ProductCatalog.Core.Models;

namespace ProductCatalog.Core.Services
{
    public interface IOrderService
    {
        Task<ICollection<OrderDto>> GetAsync();
        Task<ExecResult<IEnumerable<ChangeCoinDto>>> CreateAsync(CreateOrderDto model);
    }
}