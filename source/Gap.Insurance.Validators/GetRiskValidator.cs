using FluentValidation;
using Gap.Insurance.Model;

namespace Gap.Insurance.Validators
{
    public class GetRiskValidator : AbstractValidator<GetRiskPayload>
    {
        public GetRiskValidator()
        {
            RuleFor(payload => payload.RiskId)
                .GreaterThan(0);
        }
    }
}
