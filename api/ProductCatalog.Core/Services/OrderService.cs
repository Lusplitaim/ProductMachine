using ProductCatalog.Core.Data;
using ProductCatalog.Core.DTOs.Order;
using ProductCatalog.Core.Exceptions;
using ProductCatalog.Core.Models;
using ProductCatalog.Core.Storages;

namespace ProductCatalog.Core.Services
{
    internal class OrderService : IOrderService
    {
        private readonly IUnitOfWork m_UnitOfWork;
        private readonly IOrderStorage m_OrderStorage;
        public OrderService(IOrderStorage orderStorage, IUnitOfWork uow)
        {
            m_UnitOfWork = uow;
            m_OrderStorage = orderStorage;
        }

        public async Task<ExecResult> CreateAsync(CreateOrderDto model)
        {
            try
            {
                var result = new ExecResult();

                using var transaction = m_UnitOfWork.BeginTransaction();

                var createResult = await m_OrderStorage.CreateAsync(model);
                result.AddErrors(createResult);

                transaction.Commit();

                return result;
            }
            catch (Exception ex) when (ex is not RestCoreException)
            {
                throw new Exception("Failed to create product", ex);
            }
        }

        public async Task<ICollection<OrderDto>> GetAsync()
        {
            try
            {
                var result = await m_OrderStorage.GetAsync();
                return result;
            }
            catch (Exception ex) when (ex is not RestCoreException)
            {
                throw new Exception("Failed to get products", ex);
            }
        }
    }
}
