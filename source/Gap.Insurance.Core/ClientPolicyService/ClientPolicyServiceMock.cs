using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using Celerik.NetCore.Services;
using Gap.Insurance.EntityFramework;
using Gap.Insurance.Resources;
using Microsoft.EntityFrameworkCore;

namespace Gap.Insurance.Core
{
    [SuppressMessage("Design", "CA1062:Validate arguments of public methods", Justification = "The payload is validated in the call to Validate(payload)")]
    public class ClientPolicyServiceMock<TLoggerCategory>
        : ClientPolicyServiceBase<TLoggerCategory, DbContext>
    {
        public ClientPolicyServiceMock(
            ApiServiceArgs<TLoggerCategory> args,
            IMasterDataService masterDataSvc,
            IClientService clientSvc)
            : base(args, masterDataSvc, clientSvc) { }

        protected override async Task<bool> CheckPolicyIdUsage(int policyId)
        {
            var isInUse = MockData.ClientPolicies
                .Any(cp => cp.PolicyId == policyId);

            return await Task.FromResult(isInUse);
        }

        protected override async Task<bool> CheckPolicyIdExists(int policyId)
        {
            var exists = MockData.Policies
                .Any(p => p.PolicyId == policyId);

            return await Task.FromResult(exists);
        }

        protected override async Task<ClientPolicy> GetClientPolicyById(int clientPolicyId)
        {
            var policy = MockData.ClientPolicies
                .FirstOrDefault(cp => cp.ClientPolicyId == clientPolicyId);

            return await Task.FromResult(policy);
        }

        protected override async Task<IEnumerable<ClientPolicy>> GetClientPoliciesByClientId(int clientId)
        {
            var policies = MockData.ClientPolicies
                .Where(cp => cp.ClientId == clientId);

            return await Task.FromResult(policies);
        }

        protected override async Task SaveAsync(ApiChangeAction operation, object entity, bool commit = true)
        {
            if (operation == ApiChangeAction.Insert)
            {
                var clientPolicy = entity as ClientPolicy;
                clientPolicy.ClientPolicyId = MockData.ClientPolicies
                    .Max(p => p.ClientPolicyId) + 1;

                MockData.AddRelatedData(clientPolicy);
                MockData.ClientPolicies.Add(clientPolicy);
            }

            await Task.FromResult(0);
        }
    }
}
