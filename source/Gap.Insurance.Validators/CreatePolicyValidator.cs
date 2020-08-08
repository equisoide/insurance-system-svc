using FluentValidation;
using Gap.Insurance.Model;

namespace Gap.Insurance.Validators
{
    public class CreatePolicyValidator : AbstractValidator<CreatePolicyPayload>
    {
        public CreatePolicyValidator()
        {
            RuleFor(payload => payload.RiskId)
                .GreaterThan(0);

            RuleFor(payload => payload.Name)
                .NotNull()
                .NotEmpty()
                .MaximumLength(50);

            RuleFor(payload => payload.Description)
                .NotNull()
                .NotEmpty()
                .MaximumLength(250);

            RuleFor(payload => payload.Periods)
                .GreaterThan(0);

            RuleFor(payload => payload.Price)
                .GreaterThan(0);
        }
    }
}
