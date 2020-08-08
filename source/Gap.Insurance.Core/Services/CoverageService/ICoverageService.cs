using System.Threading.Tasks;
using Celerik.NetCore.Services;
using Gap.Insurance.Model;

namespace Gap.Insurance.Services
{
    public interface ICoverageService
    {
        Task<ApiResponse<PolicyCoverageDto, CreatePolicyCoverageStatus>> CreatePolicyCoverageAsync(CreatePolicyCoveragePayload payload);
        Task<ApiResponse<PolicyCoverageDto, UpdatePolicyCoverageStatus>> UpdatePolicyCoverageAsync(UpdatePolicyCoveragePayload payload);
        Task<ApiResponse<PolicyCoverageDto, DeletePolicyCoverageStatus>> DeletePolicyCoverageAsync(DeletePolicyCoveragePayload payload);
    }
}
