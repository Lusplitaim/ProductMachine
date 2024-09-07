using ProductCatalog.Core.Data.Entities;
using ProductCatalog.Core.DTOs.Coin;

namespace ProductCatalog.Core.Storages.Managers
{
    public class CreateOrderResult
    {
        public OrderEntity Order { get; set; }
        public ICollection<ChangeCoinDto> Coins { get; set; } = [];
    }
}
