using System;
using FluentValidation;
using Gap.Insurance.Model;

namespace Gap.Insurance.Validators
{
    public class CreateClientPolicyValidator : AbstractValidator<CreateClientPolicyPayload>
    {
        public CreateClientPolicyValidator()
        {
            RuleFor(payload => payload.ClientId)
                .GreaterThan(0);

            RuleFor(payload => payload.PolicyId)
                .GreaterThan(0);

            RuleFor(payload => payload.StartDate)
                .NotNull()
                .GreaterThanOrEqualTo(DateTime.Today.ToUniversalTime());
        }
    }
}
