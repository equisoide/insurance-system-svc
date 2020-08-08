using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using Celerik.NetCore.Services;
using Gap.Insurance.EntityFramework;
using Gap.Insurance.Model;
using Gap.Insurance.Resources;
using Microsoft.EntityFrameworkCore;

namespace Gap.Insurance.Core
{
    [SuppressMessage("Design", "CA1062:Validate arguments of public methods", Justification = "The payload is validated in the call to Validate(payload)")]
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

            var policies = (await GetPoliciesFromSourceAsync()).OrderBy(p => p.Name);
            var response = Ok<IEnumerable<PolicyDto>, GetPoliciesStatus>(policies, GetPoliciesStatus.Ok);

            EndLog();
            return response;
        }

        public async Task<ApiResponse<PolicyDto, CreatePolicyStatus>> CreatePolicyAsync(CreatePolicyPayload payload)
        {
            StartLog();
            ApiResponse<PolicyDto, CreatePolicyStatus> response;

            if (!Validate(payload, out string message, out string property))
                response = Error<CreatePolicyStatus>(message, property);
            else
            {
                var risk = await _masterDataSvc.GetRiskAsync(new GetRiskPayload { RiskId = payload.RiskId });

                if (risk.StatusCode != GetRiskStatus.Ok)
                    response = Error(CreatePolicyStatus.RiskIdNotFound);
                else
                {
                    var policy = await GetPolicyFromSourceAsync(payload.Name);

                    if (policy != null)
                        response = Error(CreatePolicyStatus.NameAlreadyTaken);
                    else
                    {

                        policy = Mapper.Map<Policy>(payload);
                        await SaveAsync(ApiChangeAction.Insert, policy);
                        response = Ok<PolicyDto, CreatePolicyStatus>(policy, CreatePolicyStatus.CreatePolicyOk);
                    }
                }
            }

            EndLog();
            return response;
        }

        public Task<ApiResponse<PolicyDto, UpdatePolicyStatus>> UpdatePolicyAsync(UpdatePolicyPayload payload)
        {
            throw new System.NotImplementedException();
        }

        public Task<ApiResponse<PolicyDto, DeletePolicyStatus>> DeletePolicyAsync(DeletePolicyPayload payload)
        {
            throw new System.NotImplementedException();
        }

        protected abstract Task<Policy> GetPolicyFromSourceAsync(string name);
        protected abstract Task<IEnumerable<Policy>> GetPoliciesFromSourceAsync();
    }
}
