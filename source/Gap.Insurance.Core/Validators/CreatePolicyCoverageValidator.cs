using FluentValidation;
using Gap.Insurance.Model;

namespace Gap.Insurance.Core
{
    public class CreatePolicyCoverageValidator : AbstractValidator<CreatePolicyCoveragePayload>
    {
        public CreatePolicyCoverageValidator()
        {
            RuleFor(payload => payload.PolicyId)
                .GreaterThan(0);

            RuleFor(payload => payload.CoverageId)
                .GreaterThan(0);

            RuleFor(payload => payload.Percentage)
                .GreaterThan(0)
                .LessThanOrEqualTo(100);
        }
    }
}
