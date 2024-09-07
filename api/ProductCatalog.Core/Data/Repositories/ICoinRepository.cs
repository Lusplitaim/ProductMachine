using ProductCatalog.Core.Data.Entities;

namespace ProductCatalog.Core.Data.Repositories
{
    public interface ICoinRepository
    {
        Task<ICollection<CoinEntity>> GetAsync();
        Task<ICollection<CoinEntity>> GetAsync(IEnumerable<int> nominals);
    }
}
