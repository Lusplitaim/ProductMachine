using ProductCatalog.Core.DTOs.Coin;

namespace ProductCatalog.Core.Services
{
    public interface ICoinService
    {
        Task<IEnumerable<CoinDto>> GetAsync();
    }
}