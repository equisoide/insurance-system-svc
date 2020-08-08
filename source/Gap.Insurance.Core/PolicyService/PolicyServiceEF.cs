using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using Celerik.NetCore.Services;
using Gap.Insurance.EntityFramework;
using Microsoft.EntityFrameworkCore;

namespace Gap.Insurance.Core
{
    [ExcludeFromCodeCoverage]
    public class PolicyServiceEF<TLoggerCategory>
        : PolicyServiceBase<TLoggerCategory, InsuranceDbContext>
    {
        public PolicyServiceEF(ApiServiceArgsEF<TLoggerCategory, InsuranceDbContext> args, IMasterDataService masterDataSvc)
            : base(args, masterDataSvc) { }

        protected override async Task<bool> ExistsPolicyId(int policyId)
            => await DbContext.Policy.AnyAsync(p => p.PolicyId == policyId);

        protected override async Task<Policy> GetPolicyById(int policyId)
            => await DbContext.Policy
                .Include(p => p.Risk)
                .Include(p => p.PolicyCoverage)
                .Include("PolicyCoverage.Coverage")
                .FirstOrDefaultAsync(p => p.PolicyId == policyId);

        protected override async Task<Policy> GetPolicyByName(string name)
            => await DbContext.Policy.FirstOrDefaultAsync(p => p.Name == name);

        protected override async Task<IEnumerable<Policy>> GetPolicies()
            => await DbContext.Policy
                .Include(p => p.Risk)
                .Include(p => p.PolicyCoverage)
                .Include("PolicyCoverage.Coverage")
                .ToListAsync();
    }
}
