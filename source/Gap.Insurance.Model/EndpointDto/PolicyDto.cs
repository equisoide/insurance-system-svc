using System.Collections.Generic;

namespace Gap.Insurance.Model
{
    public class PolicyDto
    {
        public int PolicyId { get; set; }
        public int RiskId { get; set; }
        public int RiskDescripition { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Periods { get; set; }
        public decimal Price { get; set; }

        public IEnumerable<PolicyCoverageDto> Coverages { get; set; }
    }
}
