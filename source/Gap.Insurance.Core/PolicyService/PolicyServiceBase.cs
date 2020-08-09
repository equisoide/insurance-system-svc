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
        private readonly IClientPolicyService _clientPolicySvc;

        public PolicyServiceBase(
            ApiServiceArgs<TLoggerCategory> args,
            IMasterDataService masterDataSvc,
            IClientPolicyService clientPolicySvc)
            : base(args)
        {
            _masterDataSvc = masterDataSvc;
            _clientPolicySvc = clientPolicySvc;
        }

        [ExcludeFromCodeCoverage]
        public PolicyServiceBase(
            ApiServiceArgsEF<TLoggerCategory, TDbContext> args,
            IMasterDataService masterDataSvc,
            IClientPolicyService clientPolicySvc)
            : base(args)
        {
            _masterDataSvc = masterDataSvc;
            _clientPolicySvc = clientPolicySvc;
        }

        public async Task<ApiResponse<IEnumerable<PolicyDto>, GetPoliciesStatus>> GetPoliciesAsync()
        {
            StartLog();

            var policies = (await GetPolicies()).OrderBy(p => p.Name);
            var response = Ok<IEnumerable<PolicyDto>, GetPoliciesStatus>(policies, GetPoliciesStatus.Ok);

            EndLog();
            return response;
        }

        public async Task<ApiResponse<PolicyDto, CreatePolicyStatus>> CreatePolicyAsync(CreatePolicyPayload payload)
        {
            StartLog();
            ApiResponse<PolicyDto, CreatePolicyStatus> response;

            if (!Validate(payload, out string message, out string property))
            {
                response = Error<CreatePolicyStatus>(message, property);
                EndLog();
                return response;
            }

            var risk = await _masterDataSvc.GetRiskAsync(new GetRiskPayload { RiskId = payload.RiskId });

            if (risk.StatusCode != GetRiskStatus.Ok)
            {
                response = Error(CreatePolicyStatus.RiskIdNotFound);
                EndLog();
                return response;
            }

            var policy = await GetPolicyByName(payload.Name);

            if (policy != null)
            {
                response = Error(CreatePolicyStatus.NameAlreadyTaken);
                EndLog();
                return response;
            }

            policy = Mapper.Map<Policy>(payload);
            await SaveAsync(ApiChangeAction.Insert, policy);

            response = Ok<PolicyDto, CreatePolicyStatus>(policy, CreatePolicyStatus.CreatePolicyOk);
            EndLog();

            return response;
        }

        public async Task<ApiResponse<PolicyDto, UpdatePolicyStatus>> UpdatePolicyAsync(UpdatePolicyPayload payload)
        {
            StartLog();
            ApiResponse<PolicyDto, UpdatePolicyStatus> response;

            if (!Validate(payload, out string message, out string property))
            {
                response = Error<UpdatePolicyStatus>(message, property);
                EndLog();
                return response;
            }

            if (!await ExistsPolicyId(payload.PolicyId))
            {
                response = Error(UpdatePolicyStatus.PolicyIdNotFound);
                EndLog();
                return response;
            }

            var usage = await _clientPolicySvc.CheckPolicyUsageAsync(
                new CheckPolicyUsagePayload { PolicyId = payload.PolicyId });

            if (usage.Data.IsInUse)
            {
                response = Error(UpdatePolicyStatus.PolicyInUse);
                EndLog();
                return response;
            }

            var risk = await _masterDataSvc.GetRiskAsync(
                new GetRiskPayload { RiskId = payload.RiskId });

            if (risk.StatusCode != GetRiskStatus.Ok)
            {
                response = Error(UpdatePolicyStatus.RiskIdNotFound);
                EndLog();
                return response;
            }

            var policy = await GetPolicyByName(payload.Name);

            if (policy != null && policy.PolicyId != payload.PolicyId)
            {
                response = Error(UpdatePolicyStatus.NameAlreadyTaken);
                EndLog();
                return response;
            }

            policy = Mapper.Map<Policy>(payload);
            DetachEntities();
            await SaveAsync(ApiChangeAction.Update, policy);

            policy = await GetPolicyById(payload.PolicyId);
            response = Ok<PolicyDto, UpdatePolicyStatus>(policy, UpdatePolicyStatus.UpdatePolicyOk);
            EndLog();

            return response;
        }

        public async Task<ApiResponse<PolicyDto, DeletePolicyStatus>> DeletePolicyAsync(DeletePolicyPayload payload)
        {
            StartLog();
            ApiResponse<PolicyDto, DeletePolicyStatus> response;

            if (!Validate(payload, out string message, out string property))
            {
                response = Error<DeletePolicyStatus>(message, property);
                EndLog();
                return response;
            }

            var policy = await GetPolicyById(payload.PolicyId);

            if (policy == null)
            {
                response = Error(DeletePolicyStatus.PolicyIdNotFound);
                EndLog();
                return response;
            }

            var usage = await _clientPolicySvc.CheckPolicyUsageAsync(
                new CheckPolicyUsagePayload { PolicyId = payload.PolicyId });

            if (usage.Data.IsInUse)
            {
                response = Error(DeletePolicyStatus.PolicyInUse);
                EndLog();
                return response;
            }

            await SaveAsync(ApiChangeAction.Delete, policy);

            response = Ok<PolicyDto, DeletePolicyStatus>(policy, DeletePolicyStatus.DeletePolicyOk);
            EndLog();

            return response;
        }

        protected abstract Task<bool> ExistsPolicyId(int policyId);
        protected abstract Task<Policy> GetPolicyById(int policyId);
        protected abstract Task<Policy> GetPolicyByName(string policyName);
        protected abstract Task<IEnumerable<Policy>> GetPolicies();
    }
}
