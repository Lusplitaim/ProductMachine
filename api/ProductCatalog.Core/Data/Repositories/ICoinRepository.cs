using ProductCatalog.Core.Data.Entities;

namespace ProductCatalog.Core.Data.Repositories
{
    public interface ICoinRepository
    {
        Task<ICollection<CoinEntity>> GetAsync();
    }
}
