using System.Collections.Generic;

namespace Gap.Insurance.EntityFramework
{
    public partial class PolicyStatus
    {
        public PolicyStatus()
        {
            Policy = new HashSet<Policy>();
        }

        public int PolicyStatusId { get; set; }
        public string Description { get; set; }

        public virtual ICollection<Policy> Policy { get; set; }
    }
}
