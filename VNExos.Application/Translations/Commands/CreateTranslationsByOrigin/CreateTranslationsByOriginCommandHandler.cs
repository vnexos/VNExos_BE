using AutoMapper;
using VNExos.Common.Transferer;
using VNExos.Domain.Dtos;
using VNExos.Domain.Entities;
using VNExos.Domain.Repositories;

namespace VNExos.Application.Translations.Commands.CreateTranslationsByOrigin;

public class CreateTranslationsByOriginCommandHandler(IMapper mapper, ATranslationRepository repository, ALanguageRepository languageRepository) 
    : ACommonListTransfererHandler<Translation, TranslationDto, CreateTranslationsByOriginCommand, ATranslationRepository>(mapper, repository)
{
    private readonly IMapper _mapper = mapper;
    private readonly ATranslationRepository _translationRepository = repository;
    private readonly ALanguageRepository _languageRepository = languageRepository;
    public override async Task<ICollection<TranslationDto>> Handle(CreateTranslationsByOriginCommand request, CancellationToken cancellationToken)
    {
        var origin = request.Origin;
        var languages = await _languageRepository.GetByCodeRange(request.Languages.Keys);
        var failed = new List<Translation>();
        var translations = new List<Translation>();
        foreach (var (code, transate) in request.Languages)
        {
            var language = languages.Where(r => r.Code == code).FirstOrDefault();
            if (language != null)
                translations.Add(new Translation { LanguageId = language!.Id, Origin = origin, Translate = transate, CreatedAt = DateTime.UtcNow });
            else
                failed.Add(new Translation { LanguageId = Guid.Empty, Origin = origin, Translate = transate });
        }
        var result = await _translationRepository.CreateTranslations(translations);
        foreach (var f in failed)
            result.Add(f);
        return _mapper.Map<ICollection<TranslationDto>>(result);
    }
}
