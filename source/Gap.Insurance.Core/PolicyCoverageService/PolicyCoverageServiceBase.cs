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
    public abstract class PolicyCoverageServiceBase<TLoggerCategory, TDbContext>
        : ApiServiceEF<TLoggerCategory, InsuranceResources, TDbContext>, IPolicyCoverageService
            where TDbContext : DbContext
    {
        private readonly IMasterDataService _masterDataSvc;
        private readonly IPolicyService _policyService;
        private readonly IClientPolicyService _clientPolicySvc;

        public PolicyCoverageServiceBase(
            ApiServiceArgs<TLoggerCategory> args,
            IMasterDataService masterDataSvc,
            IPolicyService policyService,
            IClientPolicyService clientPolicySvc)
            : base(args)
        {
            _masterDataSvc = masterDataSvc;
            _policyService = policyService;
            _clientPolicySvc = clientPolicySvc;
        }

        [ExcludeFromCodeCoverage]
        public PolicyCoverageServiceBase(
            ApiServiceArgsEF<TLoggerCategory, TDbContext> args,
            IMasterDataService masterDataSvc,
            IPolicyService policyService,
            IClientPolicyService clientPolicySvc)
            : base(args)
        {
            _masterDataSvc = masterDataSvc;
            _policyService = policyService;
            _clientPolicySvc = clientPolicySvc;
        }

        public async Task<ApiResponse<PolicyCoverageDto, CreatePolicyCoverageStatus>> CreatePolicyCoverageAsync(CreatePolicyCoveragePayload payload)
        {
            StartLog();
            ApiResponse<PolicyCoverageDto, CreatePolicyCoverageStatus> response;

            if (!Validate(payload, out string message, out string property))
            {
                response = Error<CreatePolicyCoverageStatus>(message, property);
                EndLog();
                return response;
            }

            var policy = await _policyService.GetPolicyAsync(
                new GetPolicyPayload { PolicyId = payload.PolicyId });

            if (policy.Data == null)
            {
                response = Error(CreatePolicyCoverageStatus.PolicyIdNotFound);
                EndLog();
                return response;
            }

            var usage = await _clientPolicySvc.CheckPolicyUsageAsync(
                new CheckPolicyUsagePayload { PolicyId = payload.PolicyId });

            if (usage.Data.IsInUse)
            {
                response = Error(CreatePolicyCoverageStatus.PolicyInUse);
                EndLog();
                return response;
            }

            var coverage = await _masterDataSvc.GetCoverageAsync(
                new GetCoveragePayload { CoverageId = payload.CoverageId });

            if (coverage.Data == null)
            {
                response = Error(CreatePolicyCoverageStatus.CoverageIdNotFound);
                EndLog();
                return response;
            }

            var coverageExists = policy.Data.Coverages.Any(
                c => c.CoverageDescription == coverage.Data.Description);

            if (coverageExists)
            {
                response = Error(CreatePolicyCoverageStatus.CoverageAlreadyAdded);
                EndLog();
                return response;
            }

            if (payload.Percentage > policy.Data.MaxCoverage)
            {
                response = new ApiResponse<PolicyCoverageDto, CreatePolicyCoverageStatus>
                {
                    Message = InsuranceResources.Get("MaxCoverageExceeded", policy.Data.MaxCoverage),
                    MessageType = ApiMessageType.Error,
                    StatusCode = CreatePolicyCoverageStatus.MaxCoverageExceeded
                };

                EndLog();
                return response;
            }

            var policyCoverage = Mapper.Map<PolicyCoverage>(payload);
            await SaveAsync(ApiChangeAction.Insert, policyCoverage);

            response = Ok<PolicyCoverageDto, CreatePolicyCoverageStatus>(policyCoverage, CreatePolicyCoverageStatus.CreatePolicyCoverageOk);
            EndLog();

            return response;
        }

        public async Task<ApiResponse<PolicyCoverageDto, UpdatePolicyCoverageStatus>> UpdatePolicyCoverageAsync(UpdatePolicyCoveragePayload payload)
        {
            StartLog();
            ApiResponse<PolicyCoverageDto, UpdatePolicyCoverageStatus> response = null;

            if (!Validate(payload, out string message, out string property))
            {
                response = Error<UpdatePolicyCoverageStatus>(message, property);
                EndLog();
                return response;
            }

            var policyCoverage = await GetPolicyCoverageById(payload.PolicyCoverageId);

            if (policyCoverage == null)
            {
                response = Error(UpdatePolicyCoverageStatus.PolicyCoverageIdNotFound);
                EndLog();
                return response;
            }

            var usage = await _clientPolicySvc.CheckPolicyUsageAsync(
                new CheckPolicyUsagePayload { PolicyId = policyCoverage.PolicyId });

            if (usage.Data.IsInUse)
            {
                response = Error(UpdatePolicyCoverageStatus.PolicyInUse);
                EndLog();
                return response;
            }

            var policy = await _policyService.GetPolicyAsync(
                new GetPolicyPayload { PolicyId = policyCoverage.PolicyId });

            if (payload.Percentage > policy.Data.MaxCoverage)
            {
                response = new ApiResponse<PolicyCoverageDto, UpdatePolicyCoverageStatus>
                {
                    Message = InsuranceResources.Get("MaxCoverageExceeded", policy.Data.MaxCoverage),
                    MessageType = ApiMessageType.Error,
                    StatusCode = UpdatePolicyCoverageStatus.MaxCoverageExceeded
                };

                EndLog();
                return response;
            }

            policyCoverage.Percentage = payload.Percentage;
            await SaveAsync(ApiChangeAction.Update, policyCoverage);

            response = Ok<PolicyCoverageDto, UpdatePolicyCoverageStatus>(policyCoverage, UpdatePolicyCoverageStatus.UpdatePolicyCoverageOk);
            EndLog();

            return response;
        }

        public async Task<ApiResponse<PolicyCoverageDto, DeletePolicyCoverageStatus>> DeletePolicyCoverageAsync(DeletePolicyCoveragePayload payload)
        {
            StartLog();
            ApiResponse<PolicyCoverageDto, DeletePolicyCoverageStatus> response;

            if (!Validate(payload, out string message, out string property))
            {
                response = Error<DeletePolicyCoverageStatus>(message, property);
                EndLog();
                return response;
            }

            var policyCoverage = await GetPolicyCoverageById(payload.PolicyCoverageId);

            if (policyCoverage == null)
            {
                response = Error(DeletePolicyCoverageStatus.PolicyCoverageIdNotFound);
                EndLog();
                return response;
            }

            var usage = await _clientPolicySvc.CheckPolicyUsageAsync(
                new CheckPolicyUsagePayload { PolicyId = policyCoverage.PolicyId });

            if (usage.Data.IsInUse)
            {
                response = Error(DeletePolicyCoverageStatus.PolicyInUse);
                EndLog();
                return response;
            }

            await SaveAsync(ApiChangeAction.Delete, policyCoverage);

            response = Ok<PolicyCoverageDto, DeletePolicyCoverageStatus>(policyCoverage, DeletePolicyCoverageStatus.DeletePolicyCoverageOk);
            EndLog();

            return response;
        }

        protected abstract Task<PolicyCoverage> GetPolicyCoverageById(int policyCoverageId);
    }
}
