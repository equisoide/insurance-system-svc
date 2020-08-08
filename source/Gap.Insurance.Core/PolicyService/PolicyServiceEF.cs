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

        protected override async Task<IEnumerable<Policy>> GetPoliciesFromSourceAsync()
            => await DbContext.Policy.ToListAsync();
    }
}
