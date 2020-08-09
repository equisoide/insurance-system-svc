using FluentValidation;
using Gap.Insurance.Model;

namespace Gap.Insurance.Validators
{
    public class SearchClientValidator : AbstractValidator<SearchClientPayload>
    {
        public SearchClientValidator()
        {
            RuleFor(payload => payload.ClientId)
                .GreaterThan(0)
                .When(payload => payload.Keyword == null);

            RuleFor(payload => payload.Keyword)
                .NotNull()
                .NotEmpty()
                .MinimumLength(3)
                .When(payload => payload.ClientId == 0);
        }
    }
}
