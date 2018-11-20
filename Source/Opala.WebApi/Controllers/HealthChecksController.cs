using System;
using Microsoft.AspNetCore.Mvc;

namespace Opala.WebApi.Controllers
{
    [ApiController]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class HealthChecksController : ControllerBase
    {
        [HttpGet("/healthz")]
        public IActionResult Healthz()
        {
            return Ok(DateTimeOffset.Now);
        }
    }
}