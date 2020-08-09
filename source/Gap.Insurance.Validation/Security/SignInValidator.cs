using FluentValidation;
using Gap.Insurance.Model;

namespace Gap.Insurance.Validation
{
    public class SignInValidator : AbstractValidator<SignInPayload>
    {
        public SignInValidator()
        {
            RuleFor(payload => payload.Email)
                .NotNull()
                .NotEmpty()
                .Length(5, 254)
                .EmailAddress();

            RuleFor(payload => payload.Password)
                .NotNull()
                .NotEmpty();
        }
    }
}
