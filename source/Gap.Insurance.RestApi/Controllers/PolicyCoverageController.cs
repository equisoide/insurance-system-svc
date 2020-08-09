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
    public class PolicyCoverageController : ControllerBase
    {
        private readonly IPolicyCoverageService _policyCoverageSvc;

        public PolicyCoverageController(IPolicyCoverageService policyCoverageSvc)
            => _policyCoverageSvc = policyCoverageSvc;

        [HttpPost]
        [Route("CreatePolicyCoverage")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<ApiResponse<PolicyCoverageDto, CreatePolicyCoverageStatus>>> CreatePolicyCoverage([FromBody]CreatePolicyCoveragePayload payload)
            => Ok(await _policyCoverageSvc.CreatePolicyCoverageAsync(payload));

        [HttpPut]
        [Route("UpdatePolicyCoverage")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<ApiResponse<PolicyCoverageDto, UpdatePolicyCoverageStatus>>> UpdatePolicyCoverage([FromBody]UpdatePolicyCoveragePayload payload)
            => Ok(await _policyCoverageSvc.UpdatePolicyCoverageAsync(payload));

        [HttpDelete]
        [Route("DeletePolicyCoverage")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<ApiResponse<PolicyCoverageDto, DeletePolicyCoverageStatus>>> DeletePolicyCoverage([FromQuery]DeletePolicyCoveragePayload payload)
            => Ok(await _policyCoverageSvc.DeletePolicyCoverageAsync(payload));
    }
}
