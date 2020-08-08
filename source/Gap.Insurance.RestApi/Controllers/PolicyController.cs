﻿using System.Collections.Generic;
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
    public class PolicyController : ControllerBase
    {
        private readonly IPolicyService _policySvc;

        public PolicyController(IPolicyService PolicySvc)
            => _policySvc = PolicySvc;

        [HttpGet]
        [Route("GetPolicies")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<ApiResponse<IEnumerable<PolicyDto>, GetPoliciesStatus>>> GetPolicies()
            => Ok(await _policySvc.GetPoliciesAsync());
    }
}
