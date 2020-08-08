﻿using System.Diagnostics.CodeAnalysis;
using Celerik.NetCore.Services;
using Celerik.NetCore.Util;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Gap.Insurance.Core
{
    public static class MasterDataServiceInjector
    {
        [ExcludeFromCodeCoverage]
        public static void AddMasterData<TLoggerCategory>(this IServiceCollection services, IConfiguration config)
        {
            var serviceType = config.GetServiceType();

            switch (serviceType)
            {
                case ApiServiceType.ServiceEF:
                    services.AddTransient<IMasterDataService, MasterDataServiceEF<TLoggerCategory>>();
                    break;
                case ApiServiceType.ServiceMock:
                    services.AddTransient<IMasterDataService, MasterDataServiceMock<TLoggerCategory>>();
                    break;
                default:
                    throw new InjectException(
                        UtilResources.Get("Common.InjectException",
                        nameof(IMasterDataService))
                    );
            }
        }
    }
}
