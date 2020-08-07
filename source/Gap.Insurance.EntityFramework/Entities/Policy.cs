using System;
using System.Collections.Generic;

namespace Gap.Insurance.EntityFramework
{
    public partial class Policy
    {
        public Policy()
        {
            PolicyCoverage = new HashSet<PolicyCoverage>();
        }

        public int PolicyId { get; set; }
        public int ClientId { get; set; }
        public int RiskId { get; set; }
        public int PolicyStatusId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public int Periods { get; set; }
        public decimal Price { get; set; }

        public virtual Client Client { get; set; }
        public virtual PolicyStatus PolicyStatus { get; set; }
        public virtual Risk Risk { get; set; }
        public virtual ICollection<PolicyCoverage> PolicyCoverage { get; set; }
    }
}
