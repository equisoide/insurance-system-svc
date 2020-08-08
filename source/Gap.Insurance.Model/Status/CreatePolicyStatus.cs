namespace Gap.Insurance.Model
{
    public enum CreatePolicyStatus
    {
        BadRequest = 400,
        RiskIdFormat = 462,
        NameFormat = 463,
        DescriptionFormat = 464,
        PeriodsFormat = 465,
        PriceFormat = 466,
        RiskIdNotFound = 485,
        CreatePolicyOk = 200
    }
}
