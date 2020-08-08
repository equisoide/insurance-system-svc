using Celerik.NetCore.Web;
using Gap.Insurance.Core;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Gap.Insurance.RestApi
{
    public class Startup
    {
        private readonly IConfiguration _config;

        public Startup(IConfiguration config)
            => _config = config;

        private string ApiName => _config["ApiName"];

        public void ConfigureServices(IServiceCollection services) =>
            MicroserviceStartup.ConfigureServices(services, _config, ApiName, () =>
            {
                services.AddCoreServices<IPolicyService>(_config);
            });

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
            => MicroserviceStartup.Configure(app, env, _config, ApiName);
    }
}
