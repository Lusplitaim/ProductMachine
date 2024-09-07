using Microsoft.AspNetCore.Mvc;
using ProductCatalog.Core.Services;

namespace ProductCatalog.API.Controllers
{
    public class CoinsController : BaseController
    {
        private readonly ICoinService m_CoinService;
        public CoinsController(ICoinService coinService)
        {
            m_CoinService = coinService;
        }

        [HttpGet]
        public async Task<IActionResult> GetCoins()
        {
            var result = await m_CoinService.GetAsync();
            return Ok(result);
        }
    }
}
