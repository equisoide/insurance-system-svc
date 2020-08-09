using System.Diagnostics.CodeAnalysis;
using Celerik.NetCore.Services;
using Celerik.NetCore.Util;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Gap.Insurance.Core
{
    public static class SecurityServiceInjector
    {
        [ExcludeFromCodeCoverage]
        public static void AddSecurity<TLoggerCategory>(this IServiceCollection services, IConfiguration config)
        {
            var serviceType = config.GetServiceType();

            switch (serviceType)
            {
                case ApiServiceType.ServiceEF:
                    services.AddTransient<ISecurityService, SecurityServiceFirebase<TLoggerCategory>>();
                    break;
                case ApiServiceType.ServiceMock:
                    services.AddTransient<ISecurityService, SecurityServiceMock<TLoggerCategory>>();
                    break;
                default:
                    throw new InjectException(
                        UtilResources.Get("Common.InjectException",
                        nameof(ISecurityService))
                    );
            }
        }
    }
}
