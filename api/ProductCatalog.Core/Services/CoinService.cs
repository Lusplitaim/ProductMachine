using NLog.Filters;
using ProductCatalog.Core.DTOs.Coin;
using ProductCatalog.Core.Exceptions;
using ProductCatalog.Core.Managers;
using ProductCatalog.Core.Storages;

namespace ProductCatalog.Core.Services
{
    internal class CoinService : ICoinService
    {
        private readonly ILoggerManager m_Logger;
        private readonly ICoinStorage m_CoinStorage;
        public CoinService(ILoggerManager logger, ICoinStorage coinStorage)
        {
            m_Logger = logger;
            m_CoinStorage = coinStorage;
        }

        public async Task<IEnumerable<CoinDto>> GetAsync()
        {
            try
            {
                var result = await m_CoinStorage.GetAsync();
                return result;
            }
            catch (Exception ex) when (ex is not RestCoreException)
            {
                m_Logger.LogError(ex, ex.Message);
                throw new Exception("Failed to get products", ex);
            }
        }
    }
}
