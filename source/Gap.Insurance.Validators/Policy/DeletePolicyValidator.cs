using FluentValidation;
using Gap.Insurance.Model;

namespace Gap.Insurance.Validators
{
    public class DeletePolicyValidator : AbstractValidator<DeletePolicyPayload>
    {
        public DeletePolicyValidator()
        {
            RuleFor(payload => payload.PolicyId)
                .GreaterThan(0);
        }
    }
}
