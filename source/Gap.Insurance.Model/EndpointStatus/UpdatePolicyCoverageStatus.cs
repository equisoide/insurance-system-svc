namespace Gap.Insurance.Model
{
    public enum UpdatePolicyCoverageStatus
    {
        BadRequest = 400,
        PolicyCoverageIdFormat = 461,
        PercentageFormat = 462,
        PolicyCoverageIdNotFound = 484,
        MaxCoverageExceeded = 487,
        PolicyInUse = 488,
        UpdatePolicyCoverageOk = 200
    }
}
