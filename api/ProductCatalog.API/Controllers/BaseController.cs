using Microsoft.AspNetCore.Mvc;
using ProductCatalog.Core.Filters;

namespace ProductCatalog.API.Controllers
{
    [ApiController]
    [TypeFilter<RestExceptionFilter>]
    [Route("api/[controller]")]
    public class BaseController : ControllerBase
    {
    }
}
