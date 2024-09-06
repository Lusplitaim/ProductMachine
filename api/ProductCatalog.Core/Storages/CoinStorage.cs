using ProductCatalog.Core.Data;
using ProductCatalog.Core.DTOs.Coin;

namespace ProductCatalog.Core.Storages
{
    internal class CoinStorage : ICoinStorage
    {
        private readonly IUnitOfWork m_UnitOfWork;
        public CoinStorage(IUnitOfWork uow)
        {
            m_UnitOfWork = uow;
        }

        public async Task<ICollection<CoinDto>> GetAsync()
        {
            var entities = await m_UnitOfWork.CoinRepository.GetAsync();

            return entities.Select(CoinDto.From).ToList();
        }
    }
}
