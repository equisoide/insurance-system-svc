using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using Celerik.NetCore.Services;
using Gap.Insurance.Model;
using Gap.Insurance.Resources;
using Microsoft.EntityFrameworkCore;

namespace Gap.Insurance.Core
{
    [SuppressMessage("Design", "CA1062:Validate arguments of public methods", Justification = "The payload is validated in the call to Validate(payload)")]
    public abstract class ClientPolicyServiceBase<TLoggerCategory, TDbContext>
        : ApiServiceEF<TLoggerCategory, InsuranceResources, TDbContext>, IClientPolicyService
            where TDbContext : DbContext
    {
        private readonly IMasterDataService _masterDataSvc;

        public ClientPolicyServiceBase(
            ApiServiceArgs<TLoggerCategory> args,
            IMasterDataService masterDataSvc)
            : base(args)
        {
            _masterDataSvc = masterDataSvc;
        }

        [ExcludeFromCodeCoverage]
        public ClientPolicyServiceBase(
            ApiServiceArgsEF<TLoggerCategory, TDbContext> args,
            IMasterDataService masterDataSvc)
            : base(args)
        {
            _masterDataSvc = masterDataSvc;
        }

        public async Task<ApiResponse<PolicyUsageDto, CheckPolicyUsageStatus>> CheckPolicyUsageAsync(CheckPolicyUsagePayload payload)
        {
            StartLog();
            ApiResponse<PolicyUsageDto, CheckPolicyUsageStatus> response;

            if (!Validate(payload, out string message, out string property))
                response = Error<CheckPolicyUsageStatus>(message, property);
            else
            {
                var isInUse = await CheckPolicyUsage(payload.PolicyId);
                var dto = new PolicyUsageDto { IsInUse = isInUse };
                response = Ok<PolicyUsageDto, CheckPolicyUsageStatus>(dto, CheckPolicyUsageStatus.Ok);
            }

            EndLog();
            return response;
        }

        public Task<ApiResponse<IEnumerable<ClientPolicyDto>, GetClientPoliciesStatus>> GetClientPoliciesAsync(GetClientPoliciesPayload payload)
        {
            throw new System.NotImplementedException();
        }

        public Task<ApiResponse<ClientPolicyDto, CreateClientPolicyStatus>> CreateClientPolicyAsync(CreatePolicyPayload payload)
        {
            throw new System.NotImplementedException();
        }

        public Task<ApiResponse<ClientPolicyDto, CancelClientPolicyStatus>> CancelClientPolicyAsync(UpdatePolicyPayload payload)
        {
            throw new System.NotImplementedException();
        }

        protected abstract Task<bool> CheckPolicyUsage(int policyId);
    }
}
