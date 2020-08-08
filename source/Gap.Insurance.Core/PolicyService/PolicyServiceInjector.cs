using System.Diagnostics.CodeAnalysis;
using Celerik.NetCore.Services;
using Celerik.NetCore.Util;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Gap.Insurance.Core
{
    public static class PolicyServiceInjector
    {
        [ExcludeFromCodeCoverage]
        public static void AddPolicy<TLoggerCategory>(this IServiceCollection services, IConfiguration config)
        {
            var serviceType = config.GetServiceType();

            switch (serviceType)
            {
                case ApiServiceType.ServiceEF:
                    services.AddTransient<IPolicyService, PolicyServiceEF<TLoggerCategory>>();
                    break;
                case ApiServiceType.ServiceMock:
                    services.AddTransient<IPolicyService, PolicyServiceMock<TLoggerCategory>>();
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
