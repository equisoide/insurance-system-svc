using FluentValidation;
using Gap.Insurance.Model;

namespace Gap.Insurance.Validators
{
    public class UpdatePolicyCoverageValidator : AbstractValidator<UpdatePolicyCoveragePayload>
    {
        public UpdatePolicyCoverageValidator()
        {
            RuleFor(payload => payload.PolicyCoverageId)
                .GreaterThan(0);
        }
    }
}
