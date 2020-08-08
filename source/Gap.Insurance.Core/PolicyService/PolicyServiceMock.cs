using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using Celerik.NetCore.Services;
using Celerik.NetCore.Util;
using Gap.Insurance.EntityFramework;
using Gap.Insurance.Resources;
using Microsoft.EntityFrameworkCore;

namespace Gap.Insurance.Core
{
    [SuppressMessage("Design", "CA1062:Validate arguments of public methods", Justification = "The payload is validated in the call to Validate(payload)")]
    public class PolicyServiceMock<TLoggerCategory>
        : PolicyServiceBase<TLoggerCategory, DbContext>
    {
        private ICollection<Policy> _policies;

        public PolicyServiceMock(ApiServiceArgs<TLoggerCategory> args, IMasterDataService masterDataSvc)
            : base(args, masterDataSvc) => LoadMockedData();

        private void LoadMockedData()
        {
            _policies = EmbeddedFileUtility.ReadJson<ICollection<Policy>>(
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

        protected override async Task<Policy> GetPolicyFromSourceAsync(string name)
        {
            var policy = _policies.FirstOrDefault(p => p.Name == name);
            return await Task.FromResult(policy);
        }

        protected override async Task<IEnumerable<Policy>> GetPoliciesFromSourceAsync()
            => await Task.FromResult(_policies);

        protected override async Task SaveAsync(ApiChangeAction operation, object entity, bool commit = true)
        {
            if (operation == ApiChangeAction.Insert)
            {
                var policy = entity as Policy;
                policy.PolicyId = _policies.Max(p => p.PolicyId) + 1;
                _policies.Add(policy);
            }

            await Task.FromResult(0);
        }
    }
}
