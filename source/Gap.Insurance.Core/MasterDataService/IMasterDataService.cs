using System.Collections.Generic;
using System.Threading.Tasks;
using Celerik.NetCore.Services;
using Gap.Insurance.Model;

namespace Gap.Insurance.Core
{
    public interface IMasterDataService
    {
        Task<ApiResponse<RiskDto, GetRiskStatus>> GetRiskAsync(GetRiskPayload payload);
        Task<ApiResponse<IEnumerable<RiskDto>, GetRisksStatus>> GetRisksAsync();
        Task<ApiResponse<CoverageDto, GetCoverageStatus>> GetCoverageAsync(GetCoveragePayload payload);
        Task<ApiResponse<IEnumerable<CoverageDto>, GetCoveragesStatus>> GetCoveragesAsync();
    }
}
