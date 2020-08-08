using System.Collections.Generic;

namespace Gap.Insurance.EntityFramework
{
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
