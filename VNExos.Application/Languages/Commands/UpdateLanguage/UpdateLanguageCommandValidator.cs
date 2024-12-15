using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VNExos.Application.Languages.Commands.UpdateLanguage;

public class UpdateLanguageCommandValidator : AbstractValidator<UpdateLanguageCommand>
{
    public UpdateLanguageCommandValidator()
    {
        RuleFor(lang => lang.Code)
            .Matches("^[a-zA-Z]{2}-[a-zA-Z]{2}$")
                .WithMessage("LANGUAGE_CODE_WRONG_FORMAT")
                .When(lang => lang.Code != null);

        RuleFor(lang => lang.FlagUrl)
            .Matches("^(https?:\\/\\/)?([a-zA-Z0-9\\-]+\\.)+[a-zA-Z]{2,6}(\\/[a-zA-Z0-9\\-._~:/?#[\\]@!$&'()*+,;=]*)?$")
                .WithMessage("LANGUAGE_FLAG_NOT_VALID")
                .When(lang => lang.FlagUrl != null);
    }
}
