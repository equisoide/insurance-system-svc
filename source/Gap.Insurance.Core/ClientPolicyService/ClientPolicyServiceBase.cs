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
                var isInUse = await CheckPolicyIdUsage(payload.PolicyId);

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
                var policies = await GetClientPoliciesByClientId(payload.ClientId);
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

        public async Task<ApiResponse<ClientPolicyDto, CreateClientPolicyStatus>> CreateClientPolicyAsync(CreateClientPolicyPayload payload)
        {
            StartLog();
            ApiResponse<ClientPolicyDto, CreateClientPolicyStatus> response;

            if (!Validate(payload, out string message, out string property))
            {
                response = Error<CreateClientPolicyStatus>(message, property);
                EndLog();
                return response;
            }

            var client = await _clientSvc.SearchClientAsync(
                new SearchClientPayload { ClientId = payload.ClientId });

            if (!client.Data.Any())
            {
                response = Error(CreateClientPolicyStatus.ClientIdNotFound);
                EndLog();
                return response;
            }

            if (!await CheckPolicyIdExists(payload.PolicyId))
            {
                response = Error(CreateClientPolicyStatus.PolicyIdNotFound);
                EndLog();
                return response;
            }

            var clientPolicy = Mapper.Map<ClientPolicy>(payload);
            await SaveAsync(ApiChangeAction.Insert, clientPolicy);
            clientPolicy = await GetClientPolicyById(clientPolicy.ClientPolicyId);

            response = Ok<ClientPolicyDto, CreateClientPolicyStatus>(clientPolicy, CreateClientPolicyStatus.CreateClientPolicyOk);
            EndLog();

            return response;
        }

        public async Task<ApiResponse<ClientPolicyDto, CancelClientPolicyStatus>> CancelClientPolicyAsync(CancelClientPolicyPayload payload)
        {
            StartLog();
            ApiResponse<ClientPolicyDto, CancelClientPolicyStatus> response;

            if (!Validate(payload, out string message, out string property))
            {
                response = Error<CancelClientPolicyStatus>(message, property);
                EndLog();
                return response;
            }

            var clientPolicy = await GetClientPolicyById(payload.ClientPolicyId);

            if (clientPolicy == null)
            {
                response = Error(CancelClientPolicyStatus.ClientPolicyIdNotFound);
                EndLog();
                return response;
            }

            if (clientPolicy.PolicyStatusId != Constants.ActivePolicyStatusId)
            {
                response = Error(CancelClientPolicyStatus.ClientPolicyNotActive);
                EndLog();
                return response;
            }

            clientPolicy.PolicyStatusId = Constants.CancelledPolicyStatusId;
            await SaveAsync(ApiChangeAction.Update, clientPolicy);
            clientPolicy = await GetClientPolicyById(payload.ClientPolicyId);

            response = Ok<ClientPolicyDto, CancelClientPolicyStatus>(clientPolicy, CancelClientPolicyStatus.CancelClientPolicyOk);
            EndLog();

            return response;
        }

        protected abstract Task<bool> CheckPolicyIdUsage(int policyId);
        protected abstract Task<bool> CheckPolicyIdExists(int policyId);
        protected abstract Task<ClientPolicy> GetClientPolicyById(int clientPolicyId);
        protected abstract Task<IEnumerable<ClientPolicy>> GetClientPoliciesByClientId(int clientId);
    }
}
