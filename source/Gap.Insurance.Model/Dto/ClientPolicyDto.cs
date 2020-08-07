using System;

namespace Gap.Insurance.Model
{
    public class ClientPolicyDto
    {
        public int ClientPolicyId { get; set; }
        public string PolicyName { get; set; }
        public string PolicyStatusDescription { get; set; }
        public DateTime StartDate { get; set; }
    }
}
