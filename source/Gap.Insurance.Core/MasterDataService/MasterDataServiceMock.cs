﻿using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Celerik.NetCore.Services;
using Gap.Insurance.EntityFramework;
using Gap.Insurance.Model;
using Gap.Insurance.Resources;
using Microsoft.EntityFrameworkCore;

namespace Gap.Insurance.Core
{
    public class MasterDataServiceMock<TLoggerCategory>
        : MasterDataServiceBase<TLoggerCategory, DbContext>
    {
        public MasterDataServiceMock(ApiServiceArgs<TLoggerCategory> args)
            : base(args) { }

        public override async Task<Risk> GetRisk(GetRiskPayload payload)
        {
            var risk = MockData.Risks.FirstOrDefault(r => r.RiskId == payload.RiskId);
            return await Task.FromResult(risk);
        }

        protected override async Task<IEnumerable<Risk>> GetRisks()
            => await Task.FromResult(MockData.Risks);

        public override async Task<Coverage> GetCoverage(GetCoveragePayload payload)
        {
            var coverage = MockData.Coverages.FirstOrDefault(c => c.CoverageId == payload.CoverageId);
            return await Task.FromResult(coverage);
        }

        protected override async Task<IEnumerable<Coverage>> GetCoverages()
            => await Task.FromResult(MockData.Coverages);
    }
}
