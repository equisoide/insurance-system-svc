using FluentValidation;
using Gap.Insurance.Model;

namespace Gap.Insurance.Validators
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
