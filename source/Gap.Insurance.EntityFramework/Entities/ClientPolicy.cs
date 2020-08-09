using System;

namespace Gap.Insurance.EntityFramework
{
    public partial class ClientPolicy
    {
        public int ClientPolicyId { get; set; }
        public int ClientId { get; set; }
        public int PolicyId { get; set; }
        public int PolicyStatusId { get; set; }
        public DateTime StartDate { get; set; }

        public virtual Client Client { get; set; }
        public virtual Policy Policy { get; set; }
        public virtual PolicyStatus PolicyStatus { get; set; }
    }
}
