using Celerik.NetCore.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Gap.Insurance.Core;

namespace Gap.Insurance.Test
{
    public class InsuranceBaseTest : BaseTest
    {
        protected override void AddServices(IServiceCollection services)
        {
            var config = GetService<IConfiguration>();
            config["ServiceType"] = "ServiceMock";
            services.AddCoreServices(config);
        }
    }
}
