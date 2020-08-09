using System.Threading.Tasks;
using Celerik.NetCore.Services;
using Gap.Insurance.Core;
using Gap.Insurance.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Gap.Insurance.RestApi
{
    [ApiController]
    [Route("[controller]")]
    public class SecurityController : ControllerBase
    {
        private readonly ISecurityService _securitySvc;

        public SecurityController(ISecurityService securitySvc)
            => _securitySvc = securitySvc;

        [HttpPost]
        [Route("SignIn")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<ApiResponse<SignInDto, SignInStatus>>> SignInWithEmailPassword(SignInPayload payload)
            => Ok(await _securitySvc.SignInAsync(payload));
    }
}
