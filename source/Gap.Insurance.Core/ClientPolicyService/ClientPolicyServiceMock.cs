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
            IMasterDataService masterDataSvc)
            : base(args, masterDataSvc) { }

        protected override async Task<bool> CheckPolicyUsage(int policyId)
        {
            var isInUse = MockData.ClientPolicies.Any(cp => cp.PolicyId == policyId);
            return await Task.FromResult(isInUse);
        }

        protected override async Task<IEnumerable<ClientPolicy>> GetClientPolicies(int clientId)
        {
            var policies = MockData.ClientPolicies.Where(cp => cp.ClientId == clientId);
            return await Task.FromResult(policies);
        }
    }
}
