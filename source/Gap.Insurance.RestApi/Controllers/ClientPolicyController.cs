using System.Collections.Generic;
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
    public class ClientPolicyController : ControllerBase
    {
        private readonly IClientPolicyService _clientPolicySvc;

        public ClientPolicyController(IClientPolicyService clientPolicySvc)
            => _clientPolicySvc = clientPolicySvc;

        [HttpGet]
        [Route("GetClientPolicies")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<ApiResponse<IEnumerable<ClientPolicyDto>, GetClientPoliciesStatus>>> GetClientPolicies([FromQuery]GetClientPoliciesPayload payload)
            => Ok(await _clientPolicySvc.GetClientPoliciesAsync(payload));

        [HttpPost]
        [Route("CreateClientPolicy")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<ApiResponse<ClientPolicyDto, CreateClientPolicyStatus>>> CreateClientPolicy([FromBody]CreateClientPolicyPayload payload)
            => Ok(await _clientPolicySvc.CreateClientPolicyAsync(payload));

        [HttpDelete]
        [Route("CancelClientPolicy")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<ApiResponse<ClientPolicyDto, CancelClientPolicyStatus>>> CancelClientPolicy([FromQuery]CancelClientPolicyPayload payload)
            => Ok(await _clientPolicySvc.CancelClientPolicyAsync(payload));
    }
}
