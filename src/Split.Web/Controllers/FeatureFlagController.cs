using Microsoft.AspNetCore.Mvc;
using Split.Domain.Interfaces;
using System.Threading.Tasks;

namespace Split.Web.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FeatureFlagController : ControllerBase
    {
        private readonly ISplitService _splitService;

        public FeatureFlagController(ISplitService splitService)
        {
            _splitService = splitService;
        }

        [HttpGet]
        public async Task<ActionResult> Get()
        {
            return Ok(await _splitService.GetScopesAsync());
        }
    }
}