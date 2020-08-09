using System.Collections.Generic;
using System.Threading.Tasks;
using Celerik.NetCore.Services;
using Gap.Insurance.Core;
using Gap.Insurance.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Gap.Insurance.RestApi
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class ClientController : ControllerBase
    {
        private readonly IClientService _clientSvc;

        public ClientController(IClientService clientSvc)
            => _clientSvc = clientSvc;

        [HttpPost]
        [Route("SearchClient")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [Authorize]
        public async Task<ActionResult<ApiResponse<IEnumerable<ClientDto>, SearchClientStatus>>> SearchClient([FromBody]SearchClientPayload payload)
            => Ok(await _clientSvc.SearchClientAsync(payload));
    }
}
