using FluentValidation;
using Gap.Insurance.Model;

namespace Gap.Insurance.Validators
{
    public class DeletePolicyCoverageValidator : AbstractValidator<DeletePolicyCoveragePayload>
    {
        public DeletePolicyCoverageValidator()
        {
            RuleFor(payload => payload.PolicyCoverageId)
                .GreaterThan(0);
        }
    }
}
