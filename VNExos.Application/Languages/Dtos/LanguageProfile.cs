using VNExos.Application.Languages.Commands.CreateLanguage;
using VNExos.Application.Languages.Commands.UpdateLanguage;
using VNExos.Common.Transferer;
using VNExos.Domain.Dtos;
using VNExos.Domain.Entities;

namespace VNExos.Application.Languages.Dtos;

public class LanguageProfile : CommonProfile<Language, LanguageDto, CreateLanguageCommand, UpdateLanguageCommand>
{
    public LanguageProfile()
    {
        CreateMap<ICollection<Translation>, Dictionary<string, string>>()
            .ConvertUsing((src, dest) => src
                .Where(t => t.Origin != null && t.Translate != null)
                .ToDictionary(t => t.Origin!, t => t.Translate!));
    }
}
