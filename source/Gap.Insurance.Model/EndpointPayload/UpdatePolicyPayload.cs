namespace Gap.Insurance.Model
{
    public class UpdatePolicyPayload
    {
        public int PolicyId { get; set; }
        public int RiskId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Periods { get; set; }
        public decimal Price { get; set; }
    }
}
