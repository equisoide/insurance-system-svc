namespace Gap.Insurance.Model
{
    public enum UpdatePolicyStatus
    {
        BadRequest = 400,
        PolicyIdFormat = 461,
        RiskIdFormat = 462,
        NameFormat = 463,
        DescriptionFormat = 464,
        PeriodsFormat = 465,
        PriceFormat = 466,
        PolicyIdNotFound = 484,
        RiskIdNotFound = 485,
        NameAlreadyTaken = 486,
        UpdatePolicyOk = 200
    }
}
