namespace Gap.Insurance.Model
{
    public enum CreatePolicyCoverageStatus
    {
        BadRequest = 400,
        PolicyIdFormat = 461,
        CoverageIdFormat = 462,
        PercentageFormat = 463,
        PolicyIdNotFound = 484,
        CoverageIdNotFound = 485,
        CreatePolicyCoverageOk = 200
    }
}
