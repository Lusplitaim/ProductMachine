using ProductCatalog.Core.Data;
using ProductCatalog.Core.Data.Entities;
using ProductCatalog.Core.DTOs.Coin;
using ProductCatalog.Core.DTOs.Product;
using ProductCatalog.Core.Models;
using System.Text.Json;

namespace ProductCatalog.Core.Storages.Managers
{
    internal class AtmManager
    {
        public async Task<ExecResult<CreateOrderResult>> CreateOrderAsync(IUnitOfWork uow, CreateOrderContext context)
        {
            var result = new ExecResult<CreateOrderResult>();

            UpdateProductsMaxQuantity(context.Products, context.OrderedProducts);
            AddInsertedCoins(context.Coins, context.InsertedCoins);

            var orderEntity = CreateOrderEntity(context);

            var insertedSum = context.InsertedCoins.Sum(c => c.Nominal * c.Quantity);
            var sumForChange = insertedSum - orderEntity.Total;
            bool hasChange = TryGetChange(sumForChange, context.Coins, out var change);
            if (!hasChange)
            {
                result.AddError("Cannot give change");
                return result;
            }
            RemoveChangeCoins(context.Coins, change);

            await uow.OrderRepository.CreateAsync(orderEntity);

            result.Result = new()
            {
                Order = orderEntity,
                Coins = change,
            };

            return result;
        }

        private void UpdateProductsMaxQuantity(IEnumerable<ProductEntity> entities, IEnumerable<OrderProductDto> orderedProducts)
        {
            foreach (var entity in entities)
            {
                var ordedProduct = orderedProducts.Single(p => p.Id == entity.Id);
                entity.MaxQuantity -= ordedProduct.Quantity;
            }
        }

        private void AddInsertedCoins(IEnumerable<CoinEntity> entities, IEnumerable<InsertedCoinDto> insertedCoins)
        {
            foreach (var entity in entities)
            {
                var insertedCoin = insertedCoins.Single(p => p.Nominal == entity.Nominal);
                entity.MaxQuantity += insertedCoin.Quantity;
            }
        }

        private void RemoveChangeCoins(IEnumerable<CoinEntity> entities, IEnumerable<ChangeCoinDto> changeCoins)
        {
            foreach (var changeCoin in changeCoins)
            {
                var entity = entities.Single(p => p.Nominal == changeCoin.Nominal);
                entity.MaxQuantity -= changeCoin.Quantity;
            }
        }

        private OrderEntity CreateOrderEntity(CreateOrderContext context)
        {
            var entities = context.Products;
            var ordered = context.OrderedProducts;

            var orderedProducts = entities.Select(p =>
            {
                return new OrderedProduct
                {
                    Id = p.Id,
                    Name = p.Name,
                    Price = p.Price,
                    Quantity = ordered.Single(pr => pr.Id == p.Id).Quantity,
                    Brand = p.Brand.Name,
                };
            }).ToList();

            var total = ordered.Sum(p =>
            {
                var product = entities.Single(pr => pr.Id == p.Id);
                return product.Price * p.Quantity;
            });

            return new()
            {
                Items = JsonSerializer.Serialize(orderedProducts),
                Total = total,
            };
        }

        private bool TryGetChange(int sumForChange, IEnumerable<CoinEntity> availableCoins, out ICollection<ChangeCoinDto> change)
        {
            var result = false;

            IEnumerable<CoinEntity> coins = [.. availableCoins.OrderByDescending(c => c.Nominal)];
            change = [];
            foreach (var coin in coins)
            {
                var nominal = coin.Nominal;
                var coinsForChange = sumForChange / nominal;
                coinsForChange = coinsForChange > coin.MaxQuantity ? coin.MaxQuantity : coinsForChange;
                if (coinsForChange > 0)
                {
                    change.Add(new() { Nominal = nominal, Quantity = coinsForChange });
                    sumForChange -= nominal * coinsForChange;
                }

                if (sumForChange == 0)
                {
                    result = true;
                    return result;
                }
            }

            change.Clear();
            return result;
        }
    }
}
