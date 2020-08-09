using System.Diagnostics.CodeAnalysis;

namespace Gap.Insurance.EntityFramework
{
    [ExcludeFromCodeCoverage]
    public partial class PolicyCoverage
    {
        public int PolicyCoverageId { get; set; }
        public int PolicyId { get; set; }
        public int CoverageId { get; set; }
        public decimal Percentage { get; set; }

        public virtual Coverage Coverage { get; set; }
        public virtual Policy Policy { get; set; }
    }
}
