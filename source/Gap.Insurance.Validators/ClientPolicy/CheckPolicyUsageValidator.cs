using FluentValidation;
using Gap.Insurance.Model;

namespace Gap.Insurance.Validators
{
    public class CheckPolicyUsageValidator : AbstractValidator<CheckPolicyUsagePayload>
    {
        public CheckPolicyUsageValidator()
        {
            RuleFor(payload => payload.PolicyId)
                .GreaterThan(0);
        }
    }
}
