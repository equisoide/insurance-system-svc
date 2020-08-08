using FluentValidation;
using Gap.Insurance.Model;

namespace Gap.Insurance.Core
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
