using FluentValidation;
using Gap.Insurance.Model;

namespace Gap.Insurance.Services
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
