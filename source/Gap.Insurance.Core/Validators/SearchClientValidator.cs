using FluentValidation;
using Gap.Insurance.Model;

namespace Gap.Insurance.Core
{
    public class SearchClientValidator : AbstractValidator<SearchClientPayload>
    {
        public SearchClientValidator()
        {
            RuleFor(payload => payload.Keyword)
                .NotNull()
                .NotEmpty()
                .MinimumLength(3);
        }
    }
}
