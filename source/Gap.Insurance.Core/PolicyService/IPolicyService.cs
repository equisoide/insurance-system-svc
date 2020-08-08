using System.Collections.Generic;
using System.Threading.Tasks;
using Celerik.NetCore.Services;
using Gap.Insurance.Model;

namespace Gap.Insurance.Services
{
    public interface IPolicyService
    {
        Task<ApiResponse<IEnumerable<PolicyDto>, GetPoliciesStatus>> GetPoliciesAsync();
        Task<ApiResponse<PolicyDto, CreatePolicyStatus>> CreatePolicyAsync(CreatePolicyPayload payload);
        Task<ApiResponse<PolicyDto, UpdatePolicyStatus>> UpdatePolicyAsync(UpdatePolicyPayload payload);
        Task<ApiResponse<PolicyDto, DeletePolicyStatus>> DeletePolicyAsync(DeletePolicyPayload payload);
    }
}
