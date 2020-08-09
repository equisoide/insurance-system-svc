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
    public class MasterDataController : ControllerBase
    {
        private readonly IMasterDataService _masterDataSvc;

        public MasterDataController(IMasterDataService masterDataSvc)
            => _masterDataSvc = masterDataSvc;

        [HttpGet]
        [Route("GetRisks")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<ApiResponse<IEnumerable<RiskDto>, GetRisksStatus>>> GetRisks()
            => Ok(await _masterDataSvc.GetRisksAsync());

        [HttpGet]
        [Route("GetCoverages")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<ApiResponse<IEnumerable<CoverageDto>, GetCoveragesStatus>>> GetCoverages()
            => Ok(await _masterDataSvc.GetCoveragesAsync());
    }
}
