using FluentValidation;
using Gap.Insurance.Model;

namespace Gap.Insurance.Services
{
    public class CancelClientPolicyValidator : AbstractValidator<CancelClientPolicyPayload>
    {
        public CancelClientPolicyValidator()
        {
            RuleFor(payload => payload.ClientPolicyId)
                .GreaterThan(0);
        }
    }
}
