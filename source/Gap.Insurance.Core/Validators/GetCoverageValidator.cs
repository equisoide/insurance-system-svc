using FluentValidation;
using Gap.Insurance.Model;

namespace Gap.Insurance.Core
{
    public class GetCoverageValidator : AbstractValidator<GetCoveragePayload>
    {
        public GetCoverageValidator()
        {
            RuleFor(payload => payload.CoverageId)
                .GreaterThan(0);
        }
    }
}
