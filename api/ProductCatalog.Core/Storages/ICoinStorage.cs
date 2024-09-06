using ProductCatalog.Core.DTOs.Coin;

namespace ProductCatalog.Core.Storages
{
    public interface ICoinStorage
    {
        Task<ICollection<CoinDto>> GetAsync();
    }
}