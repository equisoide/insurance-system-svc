using System.Collections.Generic;
using System.Threading.Tasks;
using Celerik.NetCore.Services;
using Gap.Insurance.Model;

namespace Gap.Insurance.Services
{
    public interface IClientPolicyService
    {
        Task<ApiResponse<IEnumerable<ClientPolicyDto>, GetClientPoliciesStatus>> GetClientPoliciesAsync(GetClientPoliciesPayload payload);
        Task<ApiResponse<ClientPolicyDto, CreateClientPolicyStatus>> CreateClientPolicyAsync(CreatePolicyPayload payload);
        Task<ApiResponse<ClientPolicyDto, CancelClientPolicyStatus>> CancelClientPolicyAsync(UpdatePolicyPayload payload);
    }
}
