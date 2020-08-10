using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Gap.Insurance.EntityFramework
{
    [ExcludeFromCodeCoverage]
    public partial class Policy
    {
        public Policy()
        {
            ClientPolicy = new HashSet<ClientPolicy>();
            PolicyCoverage = new HashSet<PolicyCoverage>();
        }

        public int PolicyId { get; set; }
        public int RiskId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Periods { get; set; }
        public decimal Price { get; set; }

        public virtual Risk Risk { get; set; }
        public virtual ICollection<ClientPolicy> ClientPolicy { get; set; }
        public virtual ICollection<PolicyCoverage> PolicyCoverage { get; set; }
    }
}
