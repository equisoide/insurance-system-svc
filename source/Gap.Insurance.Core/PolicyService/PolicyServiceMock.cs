﻿using System.Collections.Generic;
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
    public class PolicyServiceMock<TLoggerCategory>
        : PolicyServiceBase<TLoggerCategory, DbContext>
    {
        public PolicyServiceMock(ApiServiceArgs<TLoggerCategory> args, IMasterDataService masterDataSvc)
            : base(args, masterDataSvc) { }

        protected override async Task<bool> ExistsPolicyId(int policyId)
        {
            var policy = MockData.Policies.FirstOrDefault(p => p.PolicyId == policyId);
            return await Task.FromResult(policy != null);
        }

        protected override async Task<Policy> GetPolicyById(int policyId)
        {
            var policy = MockData.Policies.FirstOrDefault(p => p.PolicyId == policyId);
            return await Task.FromResult(policy);
        }

        protected override async Task<Policy> GetPolicyByName(string name)
        {
            var policy = MockData.Policies.FirstOrDefault(p => p.Name == name);
            return await Task.FromResult(policy);
        }

        protected override async Task<IEnumerable<Policy>> GetPolicies()
            => await Task.FromResult(MockData.Policies);

        protected override async Task SaveAsync(ApiChangeAction operation, object entity, bool commit = true)
        {
            if (operation == ApiChangeAction.Insert)
            {
                var policy = entity as Policy;
                policy.PolicyId = MockData.Policies.Max(p => p.PolicyId) + 1;
                MockData.AddRelatedData(policy);
                MockData.Policies.Add(policy);
            }
            else if (operation == ApiChangeAction.Update)
            {
                var newPolicy = entity as Policy;
                var oldPolicy = MockData.Policies.First(p => p.PolicyId == newPolicy.PolicyId);
                MockData.AddRelatedData(newPolicy);
                MockData.Policies.Add(newPolicy);
                MockData.Policies.Remove(oldPolicy);
            }
            else if (operation == ApiChangeAction.Delete)
                MockData.Policies.Remove(entity as Policy);

            await Task.FromResult(0);
        }
    }
}
