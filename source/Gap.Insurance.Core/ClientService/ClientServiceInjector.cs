using System.Diagnostics.CodeAnalysis;
using Celerik.NetCore.Services;
using Celerik.NetCore.Util;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Gap.Insurance.Core
{
    public static class ClientServiceInjector
    {
        [ExcludeFromCodeCoverage]
        public static void AddClient<TLoggerCategory>(this IServiceCollection services, IConfiguration config)
        {
            var serviceType = config.GetServiceType();

            switch (serviceType)
            {
                case ApiServiceType.ServiceEF:
                    services.AddTransient<IClientService, ClientServiceEF<TLoggerCategory>>();
                    break;
                case ApiServiceType.ServiceMock:
                    services.AddTransient<IClientService, ClientServiceMock<TLoggerCategory>>();
                    break;
                default:
                    throw new InjectException(
                        UtilResources.Get("Common.InjectException",
                        nameof(IClientService))
                    );
            }
        }
    }
}
