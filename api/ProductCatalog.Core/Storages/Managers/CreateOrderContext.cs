using ProductCatalog.Core.Data.Entities;
using ProductCatalog.Core.DTOs.Coin;
using ProductCatalog.Core.DTOs.Product;

namespace ProductCatalog.Core.Storages.Managers
{
    internal class CreateOrderContext
    {
        public ICollection<OrderProductDto> OrderedProducts { get; set; } = [];
        public ICollection<ProductEntity> Products { get; set; } = [];
        public ICollection<InsertedCoinDto> InsertedCoins { get; set; } = [];
        public ICollection<CoinEntity> Coins { get; set; } = [];
    }
}
