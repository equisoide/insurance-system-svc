using System.Diagnostics.CodeAnalysis;
using Celerik.NetCore.Services;
using Microsoft.EntityFrameworkCore;

namespace Gap.Insurance.Core
{
    [SuppressMessage("Design", "CA1062:Validate arguments of public methods", Justification = "The payload is validated in the call to Validate(payload)")]
    public class PolicyCoverageServiceMock<TLoggerCategory>
        : PolicyCoverageServiceBase<TLoggerCategory, DbContext>
    {
        public PolicyCoverageServiceMock(
            ApiServiceArgs<TLoggerCategory> args,
            IMasterDataService masterDataSvc,
            IPolicyService policyService)
            : base(args, masterDataSvc, policyService) { }
    }
}
