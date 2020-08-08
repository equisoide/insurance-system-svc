using System;

namespace Gap.Insurance.Model
{
    public class CreateClientPolicyPayload
    {
        public int ClientId { get; set; }
        public int PolicyId { get; set; }
        public DateTime StartDate { get; set; }
    }
}
