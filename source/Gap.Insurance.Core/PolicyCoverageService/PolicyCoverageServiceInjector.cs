using System.Diagnostics.CodeAnalysis;
using Celerik.NetCore.Services;
using Celerik.NetCore.Util;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Gap.Insurance.Core
{
    public static class PolicyCoverageServiceInjector
    {
        [ExcludeFromCodeCoverage]
        public static void AddPolicyCoverage<TLoggerCategory>(this IServiceCollection services, IConfiguration config)
        {
            var serviceType = config.GetServiceType();

            switch (serviceType)
            {
                case ApiServiceType.ServiceEF:
                    services.AddTransient<IPolicyCoverageService, PolicyCoverageServiceEF<TLoggerCategory>>();
                    break;
                case ApiServiceType.ServiceMock:
                    services.AddTransient<IPolicyCoverageService, PolicyCoverageServiceMock<TLoggerCategory>>();
                    break;
                default:
                    throw new InjectException(
                        UtilResources.Get("Common.InjectException",
                        nameof(IPolicyService))
                    );
            }
        }
    }
}
