using System.Collections.Generic;
using System.Threading.Tasks;
using Celerik.NetCore.Services;
using Gap.Insurance.Model;

namespace Gap.Insurance.Core
{
    public interface IMasterDataService
    {
        Task<ApiResponse<IEnumerable<RiskDto>, GetRisksStatus>> GetRisksAsync();
        Task<ApiResponse<IEnumerable<CoverageDto>, GetCoveragesStatus>> GetCoveragesAsync();
    }
}
