using Celerik.NetCore.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Gap.Insurance.EntityFramework;
using Gap.Insurance.Model;

namespace Gap.Insurance.Core
{
    public static class ServiceHandler
    {
        public static void AddCoreServices(this IServiceCollection services, IConfiguration config)
        {
            services.AddCoreServices<IMasterDataService, InsuranceDbContext>(config)
                .AddAutomapper(config =>
                {
                    config.MapPayloadsToEntities();
                    config.MapEntitiesToDtos();
                })
                .AddValidators(() =>
                {
                    services.AddValidator<CancelClientPolicyPayload, CancelClientPolicyValidator>();
                    services.AddValidator<CreateClientPolicyPayload, CreateClientPolicyValidator>();
                    services.AddValidator<CreatePolicyCoveragePayload, CreatePolicyCoverageValidator>();
                    services.AddValidator<CreatePolicyPayload, CreatePolicyValidator>();
                    services.AddValidator<DeletePolicyCoveragePayload, DeletePolicyCoverageValidator>();
                    services.AddValidator<DeletePolicyPayload, DeletePolicyValidator>();
                    services.AddValidator<GetClientPoliciesPayload, GetClientPoliciesValidator>();
                    services.AddValidator<GetCoveragePayload, GetCoverageValidator>();
                    services.AddValidator<GetRiskPayload, GetRiskValidator>();
                    services.AddValidator<SearchClientPayload, SearchClientValidator>();
                    services.AddValidator<UpdatePolicyCoveragePayload, UpdatePolicyCoverageValidator>();
                    services.AddValidator<UpdatePolicyPayload, UpdatePolicyValidator>();
                })
                .AddBusinesServices(config =>
                {
                    services.AddMasterData<IMasterDataService>(config);
                });
        }
    }
}
