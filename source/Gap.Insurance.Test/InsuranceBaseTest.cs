using Celerik.NetCore.Services;
using Gap.Insurance.Core;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Gap.Insurance.Test
{
    public class InsuranceBaseTest : BaseTest
    {
        protected override void AddServices(IServiceCollection services)
        {
            var config = GetService<IConfiguration>();
            config["ServiceType"] = "ServiceMock";
            services.AddCoreServices<IPolicyService>(config);
        }
    }
}
