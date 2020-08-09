using FluentValidation;
using Gap.Insurance.Model;

namespace Gap.Insurance.Validation
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
