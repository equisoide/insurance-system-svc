using FluentValidation;
using Gap.Insurance.Model;

namespace Gap.Insurance.Services
{
    public class GetClientPoliciesValidator : AbstractValidator<GetClientPoliciesPayload>
    {
        public GetClientPoliciesValidator()
        {
            RuleFor(payload => payload.ClientId)
                .GreaterThan(0);
        }
    }
}
