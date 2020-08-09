using System.Diagnostics.CodeAnalysis;
using Celerik.NetCore.Services;
using Gap.Insurance.EntityFramework;

namespace Gap.Insurance.Core
{
    [ExcludeFromCodeCoverage]
    public class PolicyCoverageServiceEF<TLoggerCategory>
        : PolicyCoverageServiceBase<TLoggerCategory, InsuranceDbContext>
    {
        public PolicyCoverageServiceEF(
            ApiServiceArgsEF<TLoggerCategory, InsuranceDbContext> args,
            IMasterDataService masterDataSvc,
            IPolicyService policyService,
            IClientPolicyService clientPolicySvc)
            : base(args, masterDataSvc, policyService, clientPolicySvc) { }
    }
}
