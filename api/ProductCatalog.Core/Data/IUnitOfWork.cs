using Microsoft.EntityFrameworkCore.Storage;
using ProductCatalog.Core.Data.Repositories;

namespace ProductCatalog.Core.Data
{
    public interface IUnitOfWork
    {
        IProductBrandRepository ProductCategoryRepository { get; }
        IProductRepository ProductRepository { get; }
        IOrderRepository OrderRepository { get; }
        ICoinRepository CoinRepository { get; }
        Task SaveAsync();
        IDbContextTransaction BeginTransaction();
    }
}
