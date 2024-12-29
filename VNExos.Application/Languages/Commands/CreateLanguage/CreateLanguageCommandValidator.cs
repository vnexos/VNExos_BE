using FluentValidation;

namespace VNExos.Application.Languages.Commands.CreateLanguage;

public class CreateLanguageCommandValidator : AbstractValidator<CreateLanguageCommand>
{
    public CreateLanguageCommandValidator()
    {
        RuleFor(lang => lang.Code)
            .NotEmpty()
                .WithMessage("LANGUAGE_CODE_EMPTY")
            .Matches("^[a-zA-Z]{2}-[a-zA-Z]{2}$")
                .WithMessage("LANGUAGE_CODE_WRONG_FORMAT");

        RuleFor(lang => lang.Name)
            .NotEmpty()
                .WithMessage("LANGUAGE_NAME_EMPTY");

        RuleFor(lang => lang.FlagUrl)
            .NotEmpty()
                .WithMessage("LANGUAGE_FLAG_EMPTY")
            .Matches("^(https?:\\/\\/)?([a-zA-Z0-9\\-]+\\.)+[a-zA-Z]{2,6}(\\/[a-zA-Z0-9\\-._~:/?#[\\]@!$&'()*+,;=%]*)?$")
                .WithMessage("LANGUAGE_FLAG_NOT_VALID");
    }
}
