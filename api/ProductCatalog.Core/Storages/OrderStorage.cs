using ProductCatalog.Core.Data;
using ProductCatalog.Core.Models;
using ProductCatalog.Core.DTOs.Order;
using ProductCatalog.Core.Storages.Managers;

namespace ProductCatalog.Core.Storages
{
    internal class OrderStorage : IOrderStorage
    {
        private readonly IUnitOfWork m_UnitOfWork;
        public OrderStorage(IUnitOfWork uow)
        {
            m_UnitOfWork = uow;
        }

        public async Task<ICollection<OrderDto>> GetAsync()
        {
            var entities = await m_UnitOfWork.OrderRepository.GetAsync();

            return entities.Select(OrderDto.From).ToList();
        }

        public async Task<ExecResult<CreateOrderResult>> CreateAsync(CreateOrderDto model)
        {
            var result = new ExecResult<CreateOrderResult>();

            var ids = model.Products.Select(p => p.Id).ToList();
            var products = await m_UnitOfWork.ProductRepository.GetAsync(ids);
            if (!products.All(p => ids.Contains(p.Id)))
            {
                result.AddError("Some products do not exist");
                return result;
            }

            var coinNominals = model.Coins.Select(c => c.Nominal).ToList();
            var coins = await m_UnitOfWork.CoinRepository.GetAsync(coinNominals);
            if (!coins.All(c => coinNominals.Contains(c.Nominal)))
            {
                result.AddError("Some products do not exist");
                return result;
            }

            var context = new CreateOrderContext();
            context.OrderedProducts = model.Products;
            context.InsertedCoins = model.Coins;
            context.Coins = coins;
            context.Products = products;

            var atmManager = new AtmManager();
            var createResult = await atmManager.CreateOrderAsync(m_UnitOfWork, context);
            if (!createResult.Succeeded)
            {
                result.AddErrors(createResult);
                return result;
            }

            await m_UnitOfWork.SaveAsync();

            result.Result = createResult.Result;

            return result;
        }
    }
}
