using VNExos.Common.Transferer;
using VNExos.Domain.Dtos;
using VNExos.Domain.Entities;

namespace VNExos.Application.Translations.Dtos;

public class TranslationProfile : CommonProfile<Translation, TranslationDto>
{
    public TranslationProfile()
    {
        // Do nothing
    }
}
