using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Gap.Insurance.EntityFramework
{
    [ExcludeFromCodeCoverage]
    public partial class Risk
    {
        public Risk()
        {
            Policy = new HashSet<Policy>();
        }

        public int RiskId { get; set; }
        public string Description { get; set; }
        public decimal MaxCoverage { get; set; }

        public virtual ICollection<Policy> Policy { get; set; }
    }
}
