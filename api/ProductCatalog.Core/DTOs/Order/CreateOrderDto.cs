using ProductCatalog.Core.DTOs.Coin;
using ProductCatalog.Core.DTOs.Product;

namespace ProductCatalog.Core.DTOs.Order
{
    public class CreateOrderDto
    {
        public ICollection<OrderProductDto> Products { get; set; } = [];
        public ICollection<InsertedCoinDto> Coins { get; set; } = [];
    }
}
