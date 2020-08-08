using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Celerik.NetCore.Services;
using Celerik.NetCore.Util;
using Gap.Insurance.EntityFramework;
using Gap.Insurance.Model;
using Gap.Insurance.Resources;
using Microsoft.EntityFrameworkCore;

namespace Gap.Insurance.Core
{
    public class MasterDataServiceMock<TLoggerCategory>
        : MasterDataServiceBase<TLoggerCategory, DbContext>
    {
        private IEnumerable<Risk> _risks;
        private IEnumerable<Coverage> _coverages;

        public MasterDataServiceMock(ApiServiceArgs<TLoggerCategory> args)
            : base(args) => LoadMockedData();

        private void LoadMockedData()
        {
            _risks = EmbeddedFileUtility.ReadJson<IEnumerable<Risk>>(
                "MockData.Risk.json", typeof(InsuranceResources).Assembly
            );

            _coverages = EmbeddedFileUtility.ReadJson<IEnumerable<Coverage>>(
                "MockData.Coverage.json", typeof(InsuranceResources).Assembly
            );
        }

        public override async Task<Risk> GetRiskFromSourceAsync(GetRiskPayload payload)
        {
            var risk = _risks.FirstOrDefault(r => r.RiskId == payload.RiskId);
            return await Task.FromResult(risk);
        }

        protected override async Task<IEnumerable<Risk>> GetRisksFromSourceAsync()
            => await Task.FromResult(_risks);

        public override async Task<Coverage> GetCoverageFromSourceAsync(GetCoveragePayload payload)
        {
            var coverage = _coverages.FirstOrDefault(c => c.CoverageId == payload.CoverageId);
            return await Task.FromResult(coverage);
        }

        protected override async Task<IEnumerable<Coverage>> GetCoveragesFromSourceAsync()
            => await Task.FromResult(_coverages);
    }
}
