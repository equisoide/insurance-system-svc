using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Gap.Insurance.EntityFramework
{
    [ExcludeFromCodeCoverage]
    public partial class Coverage
    {
        public Coverage()
        {
            PolicyCoverage = new HashSet<PolicyCoverage>();
        }

        public int CoverageId { get; set; }
        public string Description { get; set; }

        public virtual ICollection<PolicyCoverage> PolicyCoverage { get; set; }
    }
}
