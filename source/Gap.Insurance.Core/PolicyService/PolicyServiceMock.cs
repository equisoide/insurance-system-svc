using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Celerik.NetCore.Services;
using Celerik.NetCore.Util;
using Gap.Insurance.EntityFramework;
using Gap.Insurance.Resources;
using Microsoft.EntityFrameworkCore;

namespace Gap.Insurance.Core
{
    public class PolicyServiceMock<TLoggerCategory>
        : PolicyServiceBase<TLoggerCategory, DbContext>
    {
        private IEnumerable<Policy> _policies;

        public PolicyServiceMock(ApiServiceArgs<TLoggerCategory> args, IMasterDataService masterDataSvc)
            : base(args, masterDataSvc) => LoadMockedData();

        private void LoadMockedData()
        {
            _policies = EmbeddedFileUtility.ReadJson<IEnumerable<Policy>>(
                "MockData.Policy.json", typeof(InsuranceResources).Assembly
            );

            var risks = EmbeddedFileUtility.ReadJson<IEnumerable<Risk>>(
                "MockData.Risk.json", typeof(InsuranceResources).Assembly
            );

            var coverages = EmbeddedFileUtility.ReadJson<IEnumerable<Coverage>>(
                "MockData.Coverage.json", typeof(InsuranceResources).Assembly
            );

            var policyCoverages = EmbeddedFileUtility.ReadJson<IEnumerable<PolicyCoverage>>(
                "MockData.PolicyCoverage.json", typeof(InsuranceResources).Assembly
            );

            foreach (var policy in _policies)
            {
                policy.Risk = risks.First(r => r.RiskId == policy.RiskId);
                policy.PolicyCoverage = policyCoverages.Where(pc => pc.PolicyId == policy.PolicyId).ToList();

                foreach (var policyCoverage in policy.PolicyCoverage)
                    policyCoverage.Coverage = coverages.First(c => c.CoverageId == policyCoverage.CoverageId);
            }
        }

        protected override async Task<IEnumerable<Policy>> GetPoliciesFromSourceAsync()
            => await Task.FromResult(_policies);
    }
}
