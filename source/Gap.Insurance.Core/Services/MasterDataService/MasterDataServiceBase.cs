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

        public async Task<ApiResponse<RiskDto, GetRiskStatus>> GetRiskAsync(GetRiskPayload payload)
        {
            StartLog();
            ApiResponse<RiskDto, GetRiskStatus> response;

            if (!Validate(payload, out string message, out string property))
                response = Error<GetRiskStatus>(message, property);
            else
            {
                var risk = await GetRiskFromSourceAsync(payload);

                if (risk == null)
                    response = Error(GetRiskStatus.RiskIdNotFound);
                else
                    response = Ok<RiskDto, GetRiskStatus>(risk, GetRiskStatus.Ok);
            }

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

        public async Task<ApiResponse<CoverageDto, GetCoverageStatus>> GetCoverageAsync(GetCoveragePayload payload)
        {
            StartLog();
            ApiResponse<CoverageDto, GetCoverageStatus> response;

            if (!Validate(payload, out string message, out string property))
                response = Error<GetCoverageStatus>(message, property);
            else
            {
                var coverage = await GetCoverageFromSourceAsync(payload);

                if (coverage == null)
                    response = Error(GetCoverageStatus.CoverageIdNotFound);
                else
                    response = Ok<CoverageDto, GetCoverageStatus>(coverage, GetCoverageStatus.Ok);
            }

            EndLog();
            return response;
        }

        public async Task<ApiResponse<IEnumerable<CoverageDto>, GetCoveragesStatus>> GetCoveragesAsync()
        {
            StartLog();

            var coverages = await GetCoveragesFromSourceAsync();
            var response = Ok<IEnumerable<CoverageDto>, GetCoveragesStatus>(coverages, GetCoveragesStatus.Ok);

            EndLog();
            return response;
        }

        public abstract Task<Risk> GetRiskFromSourceAsync(GetRiskPayload payload);
        protected abstract Task<IEnumerable<Risk>> GetRisksFromSourceAsync();
        public abstract Task<Coverage> GetCoverageFromSourceAsync(GetCoveragePayload payload);
        protected abstract Task<IEnumerable<Coverage>> GetCoveragesFromSourceAsync();
    }
}
