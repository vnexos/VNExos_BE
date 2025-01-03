using AutoMapper;
using VNExos.Domain.Dtos;
using VNExos.Domain.Entities;
using VNExos.Domain.Repositories;

namespace VNExos.Application.Translations.Dtos;

internal class CreateTranslations
{
    public static async Task<ICollection<TranslationDto>> CreateTranslation(ATranslationRepository rep, IMapper mapper, ICollection<Translation> trans, ICollection<Translation> failed)
    {
        var result = await rep.CreateTranslations(trans);
        foreach (var f in failed)
            result.Add(f);
        return mapper.Map<ICollection<TranslationDto>>(result);
    }
}
