using System.Diagnostics.CodeAnalysis;

namespace Gap.Insurance.Model
{
    [ExcludeFromCodeCoverage]
    public class FirebaseConfig
    {
        public string ApiKey { get; set; }
        public string AuthApiBaseUrl { get; set; }
        public string AuthApiRequestUrl { get; set; }
        public string AuthorityUrl { get; set; }
        public string ProjectId { get; set; }
    }
}
