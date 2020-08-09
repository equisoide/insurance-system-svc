using Celerik.NetCore.Services;
using Gap.Insurance.EntityFramework;
using Gap.Insurance.Model;
using Gap.Insurance.Validators;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Gap.Insurance.Core
{
    public static class ServiceHandler
    {
        public static void AddCoreServices<TLoggerCategory>(this IServiceCollection services, IConfiguration config)
        {
            services.AddCoreServices<TLoggerCategory, InsuranceDbContext>(config)
                .AddAutomapper(config =>
                {
                    config.MapPayloadsToEntities();
                    config.MapEntitiesToDtos();
                })
                .AddValidators(() =>
                {
                    services.AddValidator<CancelClientPolicyPayload, CancelClientPolicyValidator>();
                    services.AddValidator<CheckPolicyUsagePayload, CheckPolicyUsageValidator>();
                    services.AddValidator<CreateClientPolicyPayload, CreateClientPolicyValidator>();
                    services.AddValidator<CreatePolicyCoveragePayload, CreatePolicyCoverageValidator>();
                    services.AddValidator<CreatePolicyPayload, CreatePolicyValidator>();
                    services.AddValidator<DeletePolicyCoveragePayload, DeletePolicyCoverageValidator>();
                    services.AddValidator<DeletePolicyPayload, DeletePolicyValidator>();
                    services.AddValidator<GetClientPoliciesPayload, GetClientPoliciesValidator>();
                    services.AddValidator<GetCoveragePayload, GetCoverageValidator>();
                    services.AddValidator<GetPolicyPayload, GetPolicyValidator>();
                    services.AddValidator<GetRiskPayload, GetRiskValidator>();
                    services.AddValidator<SearchClientPayload, SearchClientValidator>();
                    services.AddValidator<UpdatePolicyCoveragePayload, UpdatePolicyCoverageValidator>();
                    services.AddValidator<UpdatePolicyPayload, UpdatePolicyValidator>();
                })
                .AddBusinesServices(config =>
                {
                    services.AddMasterData<TLoggerCategory>(config);
                    services.AddPolicy<TLoggerCategory>(config);
                    services.AddClient<TLoggerCategory>(config);
                    services.AddClientPolicy<TLoggerCategory>(config);
                });
        }
    }
}
