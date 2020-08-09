using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using Celerik.NetCore.Services;
using Gap.Insurance.EntityFramework;
using Microsoft.EntityFrameworkCore;

namespace Gap.Insurance.Core
{
    [ExcludeFromCodeCoverage]
    public class ClientPolicyServiceEF<TLoggerCategory>
        : ClientPolicyServiceBase<TLoggerCategory, InsuranceDbContext>
    {
        public ClientPolicyServiceEF(
            ApiServiceArgsEF<TLoggerCategory, InsuranceDbContext> args,
            IMasterDataService masterDataSvc,
            IClientService clientSvc)
            : base(args, masterDataSvc, clientSvc) { }

        protected override async Task<bool> CheckPolicyIdUsage(int policyId)
            => await DbContext.ClientPolicy.AnyAsync(cp => cp.PolicyId == policyId);

        protected override async Task<bool> CheckPolicyIdExists(int policyId)
            => await DbContext.Policy.AnyAsync(p => p.PolicyId == policyId);

        protected override async Task<ClientPolicy> GetClientPolicyById(int clientPolicyId)
            => await DbContext.ClientPolicy
                    .Where(cp => cp.ClientPolicyId == clientPolicyId)
                    .Include(cp => cp.Policy)
                    .Include(cp => cp.PolicyStatus)
                    .FirstOrDefaultAsync();

        protected override async Task<IEnumerable<ClientPolicy>> GetClientPoliciesByClientId(int clientId)
            => await DbContext.ClientPolicy
                .Where(cp => cp.ClientId == clientId)
                .Include(cp => cp.Policy)
                .Include(cp => cp.PolicyStatus)
                .ToListAsync();
    }
}
