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
    public abstract class PolicyServiceBase<TLoggerCategory, TDbContext>
        : ApiServiceEF<TLoggerCategory, InsuranceResources, TDbContext>, IPolicyService
            where TDbContext : DbContext
    {
        private readonly IMasterDataService _masterDataSvc;

        public PolicyServiceBase(ApiServiceArgs<TLoggerCategory> args, IMasterDataService masterDataSvc)
            : base(args) => _masterDataSvc = masterDataSvc;

        [ExcludeFromCodeCoverage]
        public PolicyServiceBase(ApiServiceArgsEF<TLoggerCategory, TDbContext> args, IMasterDataService masterDataSvc)
            : base(args) => _masterDataSvc = masterDataSvc;

        public async Task<ApiResponse<IEnumerable<PolicyDto>, GetPoliciesStatus>> GetPoliciesAsync()
        {
            StartLog();

            var policies = await GetPoliciesFromSourceAsync();
            var response = Ok<IEnumerable<PolicyDto>, GetPoliciesStatus>(policies, GetPoliciesStatus.Ok);

            EndLog();
            return response;
        }

        public Task<ApiResponse<PolicyDto, CreatePolicyStatus>> CreatePolicyAsync(CreatePolicyPayload payload)
        {
            throw new System.NotImplementedException();
        }

        public Task<ApiResponse<PolicyDto, UpdatePolicyStatus>> UpdatePolicyAsync(UpdatePolicyPayload payload)
        {
            throw new System.NotImplementedException();
        }

        public Task<ApiResponse<PolicyDto, DeletePolicyStatus>> DeletePolicyAsync(DeletePolicyPayload payload)
        {
            throw new System.NotImplementedException();
        }

        protected abstract Task<IEnumerable<Policy>> GetPoliciesFromSourceAsync();
    }
}
