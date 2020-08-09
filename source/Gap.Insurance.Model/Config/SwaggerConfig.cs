using System.Diagnostics.CodeAnalysis;

namespace Gap.Insurance.Model
{
    [ExcludeFromCodeCoverage]
    public class SwaggerConfig
    {
        public string ApiEndpoint { get; set; }
        public string ApiName { get; set; }
        public string ApiVersion { get; set; }
    }
}
