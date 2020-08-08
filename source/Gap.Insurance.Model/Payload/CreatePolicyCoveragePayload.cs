namespace Gap.Insurance.Model
{
    public class CreatePolicyCoveragePayload
    {
        public int PolicyId { get; set; }
        public int CoverageId { get; set; }
        public decimal Percentage { get; set; }
    }
}
