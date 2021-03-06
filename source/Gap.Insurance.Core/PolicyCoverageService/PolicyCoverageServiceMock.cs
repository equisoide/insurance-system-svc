﻿using System.Linq;
using System.Threading.Tasks;
using Celerik.NetCore.Services;
using Gap.Insurance.EntityFramework;
using Gap.Insurance.Resources;
using Microsoft.EntityFrameworkCore;

namespace Gap.Insurance.Core
{
    public class PolicyCoverageServiceMock<TLoggerCategory>
        : PolicyCoverageServiceBase<TLoggerCategory, DbContext>
    {
        public PolicyCoverageServiceMock(
            ApiServiceArgs<TLoggerCategory> args,
            IMasterDataService masterDataSvc,
            IPolicyService policyService,
            IClientPolicyService clientPolicySvc)
            : base(args, masterDataSvc, policyService, clientPolicySvc) { }

        protected override async Task<PolicyCoverage> GetPolicyCoverageById(int policyCoverageId)
        {
            var policyCoverage = MockData.PolicyCoverages
                .FirstOrDefault(pc => pc.PolicyCoverageId == policyCoverageId);

            return await Task.FromResult(policyCoverage);
        }

        protected override async Task SaveAsync(ApiChangeAction operation, object entity, bool commit = true)
        {
            if (operation == ApiChangeAction.Insert)
            {
                var policyCoverage = entity as PolicyCoverage;
                policyCoverage.PolicyCoverageId = MockData.PolicyCoverages
                    .Max(p => p.PolicyCoverageId) + 1;

                MockData.AddRelatedData(policyCoverage);
                MockData.PolicyCoverages.Add(policyCoverage);
            }
            else if (operation == ApiChangeAction.Delete)
            {
                MockData.PolicyCoverages.Remove(entity as PolicyCoverage);
            }

            await Task.FromResult(0);
        }
    }
}
