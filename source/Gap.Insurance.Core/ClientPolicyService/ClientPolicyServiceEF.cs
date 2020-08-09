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
            IMasterDataService masterDataSvc)
            : base(args, masterDataSvc) { }

        protected override async Task<bool> CheckPolicyUsage(int policyId)
            => await DbContext.ClientPolicy.AnyAsync(cp => cp.PolicyId == policyId);
    }
}
