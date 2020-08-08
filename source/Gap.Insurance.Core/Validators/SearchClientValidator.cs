using FluentValidation;
using Gap.Insurance.Model;

namespace Gap.Insurance.Services
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
