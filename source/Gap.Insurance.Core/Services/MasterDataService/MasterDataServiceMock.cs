using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Celerik.NetCore.Services;
using Celerik.NetCore.Util;
using Gap.Insurance.EntityFramework;
using Gap.Insurance.Model;
using Microsoft.EntityFrameworkCore;

namespace Gap.Insurance.Core
{
    public class MasterDataServiceMock<TLoggerCategory>
        : MasterDataServiceBase<TLoggerCategory, DbContext>
    {
        private readonly IEnumerable<Risk> _risks = null;
        private readonly IEnumerable<Coverage> _coverages = null;

        public MasterDataServiceMock(ApiServiceArgs<TLoggerCategory> args)
            : base(args)
        {
            _risks = EmbeddedFileUtility.ReadJson<IEnumerable<Risk>>(
                "Assets.Risk.Mock.json"
            );

            _coverages = EmbeddedFileUtility.ReadJson<IEnumerable<Coverage>>(
                "Assets.Coverage.Mock.json"
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
