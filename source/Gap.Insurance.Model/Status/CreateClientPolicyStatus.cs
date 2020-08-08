namespace Gap.Insurance.Model
{
    public enum CreateClientPolicyStatus
    {
        BadRequest = 400,
        ClientIdFormat = 461,
        PolicyIdFormat = 462,
        StartDateFormat = 463,
        ClientIdNotFound = 484,
        PolicyIdNotFound = 485,
        CreateClientPolicyOk = 200
    }
}
