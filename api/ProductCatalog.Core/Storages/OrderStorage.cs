using ProductCatalog.Core.Data;
using ProductCatalog.Core.Models;
using ProductCatalog.Core.DTOs.Order;

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

        public Task<ExecResult<OrderDto>> CreateAsync(CreateOrderDto model)
        {
            var result = new ExecResult<OrderDto>();

            /*ProductEntity entity = new()
            {
                Name = model.Name,
                Price = model.Price,
            };

            await m_UnitOfWork.ProductRepository.CreateAsync(entity);

            await m_UnitOfWork.SaveAsync();

            var updated = await GetAsync(entity.Id);
            result.Result = updated;*/

            return Task.FromResult(result);
        }
    }
}
