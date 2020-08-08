using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using Celerik.NetCore.Services;
using Gap.Insurance.EntityFramework;
using Gap.Insurance.Model;
using Microsoft.EntityFrameworkCore;

namespace Gap.Insurance.Core
{
    [ExcludeFromCodeCoverage]
    public class MasterDataServiceEF<TLoggerCategory>
        : MasterDataServiceBase<TLoggerCategory, InsuranceDbContext>
    {
        public MasterDataServiceEF(ApiServiceArgsEF<TLoggerCategory, InsuranceDbContext> args)
            : base(args) { }

        public override async Task<Risk> GetRiskFromSourceAsync(GetRiskPayload payload)
            => await DbContext.Risk.FirstOrDefaultAsync(r => r.RiskId == payload.RiskId);

        protected override async Task<IEnumerable<Risk>> GetRisksFromSourceAsync()
            => await DbContext.Risk.ToListAsync();

        public override async Task<Coverage> GetCoverageFromSourceAsync(GetCoveragePayload payload)
            => await DbContext.Coverage.FirstOrDefaultAsync(c => c.CoverageId == payload.CoverageId);

        protected override async Task<IEnumerable<Coverage>> GetCoveragesFromSourceAsync()
            => await DbContext.Coverage.ToListAsync();
    }
}
