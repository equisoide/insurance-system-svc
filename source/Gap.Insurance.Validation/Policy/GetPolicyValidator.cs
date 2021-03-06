﻿using FluentValidation;
using Gap.Insurance.Model;

namespace Gap.Insurance.Validation
{
    public class GetPolicyValidator : AbstractValidator<GetPolicyPayload>
    {
        public GetPolicyValidator()
        {
            RuleFor(payload => payload.PolicyId)
                .GreaterThan(0);
        }
    }
}
