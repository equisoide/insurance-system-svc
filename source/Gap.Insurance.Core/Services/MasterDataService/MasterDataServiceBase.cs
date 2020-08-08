using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using Celerik.NetCore.Services;
using Gap.Insurance.EntityFramework;
using Gap.Insurance.Model;
using Microsoft.EntityFrameworkCore;

namespace Gap.Insurance.Core
{
    public abstract class MasterDataServiceBase<TLoggerCategory, TDbContext>
        : ApiServiceEF<TLoggerCategory, CoreResources, TDbContext>, IMasterDataService
            where TDbContext : DbContext
    {
        public MasterDataServiceBase(ApiServiceArgs<TLoggerCategory> args)
            : base(args) { }

        [ExcludeFromCodeCoverage]
        public MasterDataServiceBase(ApiServiceArgsEF<TLoggerCategory, TDbContext> args)
            : base(args) { }

        public async Task<ApiResponse<IEnumerable<CoverageDto>, GetCoveragesStatus>> GetCoveragesAsync()
        {
            StartLog();

            var coverages = await GetCoveragesFromSourceAsync();
            var response = Ok<IEnumerable<CoverageDto>, GetCoveragesStatus>(coverages, GetCoveragesStatus.Ok);

            EndLog();
            return response;
        }

        public async Task<ApiResponse<IEnumerable<RiskDto>, GetRisksStatus>> GetRisksAsync()
        {
            StartLog();

            var risks = await GetRisksFromSourceAsync();
            var response = Ok<IEnumerable<RiskDto>, GetRisksStatus>(risks, GetRisksStatus.Ok);

            EndLog();
            return response;
        }

        protected abstract Task<Coverage> GetCoveragesFromSourceAsync();
        protected abstract Task<Risk> GetRisksFromSourceAsync();
    }
}
