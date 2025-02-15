using VNExos.Application.Translations.Commands.UpdateTranslationByCodeAndOrigin;
using VNExos.Application.Translations.Commands.UpdateTranslationById;
using VNExos.Common.Transferer;
using VNExos.Domain.Dtos;
using VNExos.Domain.Entities;

namespace VNExos.Application.Translations.Dtos;

public class TranslationProfile : CommonProfile<Translation, TranslationDto, UpdateTranslationByCodeAndOriginCommand, UpdateTranslationByIdCommand>
{
    public TranslationProfile()
    {
        // Do nothing
    }
}
