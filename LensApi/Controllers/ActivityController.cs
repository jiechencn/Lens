using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Web.Resource;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using Me.JieChen.Lens.Api.Auth;

namespace LensApi.Controllers
{

    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class ActivityController : ControllerBase
    {
        private readonly ILogger<ActivityController> _logger;

        public ActivityController(ILogger<ActivityController> logger)
        {
            _logger = logger;
        }

        [Authorize(Roles = AuthClaimConstants.ROLE_LENS_USER)]
        [RequiredScope(AuthClaimConstants.SCOPE_LENS_ME_READ, AuthClaimConstants.SCOPE_LENS_ALL_READ)]
        [HttpGet]
        public async Task<string> Get()
        {
            return "hello";
        }
    }
}