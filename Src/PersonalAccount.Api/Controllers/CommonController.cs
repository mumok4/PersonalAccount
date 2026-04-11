using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PersonalAccount.Common.Core;

namespace PersonalAccount.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommonController : ControllerBase
    {
        private readonly ILoadingService _loading;

        public CommonController(ILoadingService loadingService)
        {
            _loading = loadingService;
        }

        [HttpGet("version")]
        public IActionResult GetVersion()
        {
            return Ok(new { Version = "1.0.0" });
        }
    }
}