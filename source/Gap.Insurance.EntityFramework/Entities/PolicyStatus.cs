using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Gap.Insurance.EntityFramework
{
    [ExcludeFromCodeCoverage]
    public partial class PolicyStatus
    {
        public PolicyStatus()
        {
            ClientPolicy = new HashSet<ClientPolicy>();
        }

        public int PolicyStatusId { get; set; }
        public string Description { get; set; }

        public virtual ICollection<ClientPolicy> ClientPolicy { get; set; }
    }
}
