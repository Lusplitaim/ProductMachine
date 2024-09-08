using Microsoft.AspNetCore.Mvc;
using ProductCatalog.Core.DTOs.Order;
using ProductCatalog.Core.Extensions;
using ProductCatalog.Core.Services;

namespace ProductCatalog.API.Controllers
{
    public class OrdersController : BaseController
    {
        private readonly IOrderService m_OrderService;
        public OrdersController(IOrderService service)
        {
            m_OrderService = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetOrders()
        {
            var result = await m_OrderService.GetAsync();
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrder(CreateOrderDto model)
        {
            var result = await m_OrderService.CreateAsync(model);
            return this.ResolveResult(result, () => Created(nameof(CreateOrder), result.Result));
        }
    }
}
