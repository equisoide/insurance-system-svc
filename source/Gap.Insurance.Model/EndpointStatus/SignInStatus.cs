namespace Gap.Insurance.Model
{
    public enum SignInStatus
    {
        BadRequest = 400,
        EmailFormat = 461,
        PasswordFormat = 462,
        EmailNotFound = 484,
        PasswordInvalid = 486,
        UserDisabled = 490,
        EmailUnverified = 491,
        Ok = 200
    }
}
