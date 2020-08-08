using FluentValidation;
using Gap.Insurance.Model;

namespace Gap.Insurance.Core
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
