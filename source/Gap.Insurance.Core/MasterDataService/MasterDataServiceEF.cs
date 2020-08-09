using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using Celerik.NetCore.Services;
using Gap.Insurance.EntityFramework;
using Microsoft.EntityFrameworkCore;

namespace Gap.Insurance.Core
{
    [ExcludeFromCodeCoverage]
    public class MasterDataServiceEF<TLoggerCategory>
        : MasterDataServiceBase<TLoggerCategory, InsuranceDbContext>
    {
        public MasterDataServiceEF(ApiServiceArgsEF<TLoggerCategory, InsuranceDbContext> args)
            : base(args) { }

        public override async Task<Risk> GetRiskById(int riskId)
            => await DbContext.Risk.FirstOrDefaultAsync(r => r.RiskId == riskId);

        protected override async Task<IEnumerable<Risk>> GetRisks()
            => await DbContext.Risk.ToListAsync();

        public override async Task<Coverage> GetCoverageById(int coverageId)
            => await DbContext.Coverage.FirstOrDefaultAsync(c => c.CoverageId == coverageId);

        protected override async Task<IEnumerable<Coverage>> GetCoverages()
            => await DbContext.Coverage.ToListAsync();
    }
}
