﻿using System.Collections.Generic;
using System.Linq;
using Celerik.NetCore.Util;
using Gap.Insurance.EntityFramework;

namespace Gap.Insurance.Resources
{
    public static class MockData
    {
        public static IEnumerable<Risk> Risks =
            EmbeddedFileUtility.ReadJson<IEnumerable<Risk>>(
                "MockData.Risk.json", typeof(InsuranceResources).Assembly
            );

        public static IEnumerable<Coverage> Coverages =
            EmbeddedFileUtility.ReadJson<IEnumerable<Coverage>>(
                "MockData.Coverage.json", typeof(InsuranceResources).Assembly
            );

        public static IEnumerable<PolicyStatus> PolicyStatus =
            EmbeddedFileUtility.ReadJson<IEnumerable<PolicyStatus>>(
                "MockData.PolicyStatus.json", typeof(InsuranceResources).Assembly
            );

        public static ICollection<Policy> Policies =
            EmbeddedFileUtility.ReadJson<ICollection<Policy>>(
                "MockData.Policy.json", typeof(InsuranceResources).Assembly
            );

        public static ICollection<PolicyCoverage> PolicyCoverages =
            EmbeddedFileUtility.ReadJson<ICollection<PolicyCoverage>>(
                 "MockData.PolicyCoverage.json", typeof(InsuranceResources).Assembly
             );

        public static ICollection<Client> Clients =
            EmbeddedFileUtility.ReadJson<ICollection<Client>>(
                 "MockData.Client.json", typeof(InsuranceResources).Assembly
             );

        public static ICollection<ClientPolicy> ClientPolicies =
            EmbeddedFileUtility.ReadJson<ICollection<ClientPolicy>>(
                 "MockData.ClientPolicy.json", typeof(InsuranceResources).Assembly
             );

        static MockData()
        {
            foreach (var policy in Policies)
                AddRelatedData(policy);

            foreach (var clientPolicy in ClientPolicies)
                AddRelatedData(clientPolicy);
        }

        public static void AddRelatedData(Policy policy)
        {
            policy.Risk = Risks
                .First(r => r.RiskId == policy.RiskId);
            policy.PolicyCoverage = PolicyCoverages
                .Where(pc => pc.PolicyId == policy.PolicyId)
                .ToList();

            foreach (var policyCoverage in policy.PolicyCoverage)
                policyCoverage.Coverage = Coverages.First(
                    c => c.CoverageId == policyCoverage.CoverageId);
        }

        public static void AddRelatedData(PolicyCoverage policyCoverage)
        {
            policyCoverage.Policy = Policies
                .First(p => p.PolicyId == policyCoverage.PolicyId);
            policyCoverage.Coverage = Coverages
                .First(c => c.CoverageId == policyCoverage.CoverageId);
        }

        public static void AddRelatedData(ClientPolicy clientPolicy)
        {
            clientPolicy.Policy = Policies
                .First(p => p.PolicyId == clientPolicy.PolicyId);
            clientPolicy.PolicyStatus = PolicyStatus
                .First(ps => ps.PolicyStatusId == clientPolicy.PolicyStatusId);
        }
    }
}
