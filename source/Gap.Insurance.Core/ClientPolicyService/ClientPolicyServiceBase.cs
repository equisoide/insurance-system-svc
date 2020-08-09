using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using Celerik.NetCore.Services;
using Gap.Insurance.EntityFramework;
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
        private readonly IClientService _clientSvc;

        public ClientPolicyServiceBase(
            ApiServiceArgs<TLoggerCategory> args,
            IMasterDataService masterDataSvc,
            IClientService clientSvc)
            : base(args)
        {
            _masterDataSvc = masterDataSvc;
            _clientSvc = clientSvc;
        }

        [ExcludeFromCodeCoverage]
        public ClientPolicyServiceBase(
            ApiServiceArgsEF<TLoggerCategory, TDbContext> args,
            IMasterDataService masterDataSvc,
            IClientService clientSvc)
            : base(args)
        {
            _masterDataSvc = masterDataSvc;
            _clientSvc = clientSvc;
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

                response = new ApiResponse<PolicyUsageDto, CheckPolicyUsageStatus>
                {
                    Data = new PolicyUsageDto { IsInUse = isInUse },
                    StatusCode = CheckPolicyUsageStatus.Ok,
                    Success = true
                };
            }

            EndLog();
            return response;
        }

        public async Task<ApiResponse<IEnumerable<ClientPolicyDto>, GetClientPoliciesStatus>> GetClientPoliciesAsync(GetClientPoliciesPayload payload)
        {
            StartLog();
            ApiResponse<IEnumerable<ClientPolicyDto>, GetClientPoliciesStatus> response;

            if (!Validate(payload, out string message, out string property))
                response = Error<GetClientPoliciesStatus>(message, property);
            else
            {
                var policies = await GetClientPolicies(payload.ClientId);
                response = new ApiResponse<IEnumerable<ClientPolicyDto>, GetClientPoliciesStatus>
                {
                    Data = Mapper.Map<IEnumerable<ClientPolicyDto>>(policies),
                    StatusCode = GetClientPoliciesStatus.Ok,
                    Success = true
                };
            }

            EndLog();
            return response;
        }

        public Task<ApiResponse<ClientPolicyDto, CreateClientPolicyStatus>> CreateClientPolicyAsync(CreateClientPolicyPayload payload)
        {
            throw new System.NotImplementedException();
        }

        public Task<ApiResponse<ClientPolicyDto, CancelClientPolicyStatus>> CancelClientPolicyAsync(CancelClientPolicyPayload payload)
        {
            throw new System.NotImplementedException();
        }

        protected abstract Task<bool> CheckPolicyUsage(int policyId);
        protected abstract Task<IEnumerable<ClientPolicy>> GetClientPolicies(int clientId);
    }
}
