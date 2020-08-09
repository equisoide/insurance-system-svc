using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using Celerik.NetCore.Services;
using Gap.Insurance.EntityFramework;
using Gap.Insurance.Model;
using Gap.Insurance.Resources;
using Microsoft.EntityFrameworkCore;

namespace Gap.Insurance.Core
{
    public abstract class MasterDataServiceBase<TLoggerCategory, TDbContext>
        : ApiServiceEF<TLoggerCategory, InsuranceResources, TDbContext>, IMasterDataService
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
                var risk = await GetRiskById(payload.RiskId);
                response = new ApiResponse<RiskDto, GetRiskStatus>
                {
                    Data = Mapper.Map<RiskDto>(risk),
                    StatusCode = GetRiskStatus.Ok,
                    Success = true
                };

            }

            EndLog();
            return response;
        }

        public async Task<ApiResponse<IEnumerable<RiskDto>, GetRisksStatus>> GetRisksAsync()
        {
            StartLog();

            var risks = (await GetRisks()).OrderBy(r => r.RiskId);
            var response = new ApiResponse<IEnumerable<RiskDto>, GetRisksStatus>
            {
                Data = Mapper.Map<IEnumerable<RiskDto>>(risks),
                StatusCode = GetRisksStatus.Ok,
                Success = true
            };

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
                var coverage = await GetCoverageById(payload.CoverageId);
                response = new ApiResponse<CoverageDto, GetCoverageStatus>
                {
                    Data = Mapper.Map<CoverageDto>(coverage),
                    StatusCode = GetCoverageStatus.Ok,
                    Success = true
                };
            }

            EndLog();
            return response;
        }

        public async Task<ApiResponse<IEnumerable<CoverageDto>, GetCoveragesStatus>> GetCoveragesAsync()
        {
            StartLog();

            var coverages = (await GetCoverages()).OrderBy(c => c.Description);
            var response = new ApiResponse<IEnumerable<CoverageDto>, GetCoveragesStatus>
            {
                Data = Mapper.Map<IEnumerable<CoverageDto>>(coverages),
                StatusCode = GetCoveragesStatus.Ok,
                Success = true
            };

            EndLog();
            return response;
        }

        public abstract Task<Risk> GetRiskById(int riskId);
        protected abstract Task<IEnumerable<Risk>> GetRisks();
        public abstract Task<Coverage> GetCoverageById(int coverageId);
        protected abstract Task<IEnumerable<Coverage>> GetCoverages();
    }
}
