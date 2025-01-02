using AutoMapper;
using VNExos.Common.Transferer;
using VNExos.Domain.Dtos;
using VNExos.Domain.Entities;
using VNExos.Domain.Repositories;

namespace VNExos.Application.Translations.Commands.CreateTranslationsByLanguageCode;

public class CreateTranslationsByLanguageCodeCommandHandler(IMapper mapper, ATranslationRepository repository, ALanguageRepository languageRepository)
        : ACommonListTransfererHandler<Translation, TranslationDto, CreateTranslationsByLanguageCodeCommand, ATranslationRepository>(mapper, repository)
{
    private readonly IMapper _mapper = mapper;
    private readonly ATranslationRepository _translationRepository = repository;
    private readonly ALanguageRepository _languageRepository = languageRepository;

    public async override Task<ICollection<TranslationDto>> Handle(CreateTranslationsByLanguageCodeCommand request, CancellationToken cancellationToken)
    {
        var code = request.LanguageCode;
        var language = await _languageRepository.GetByCode(code!);
        var failed = new List<Translation>();
        var translations = new List<Translation>();
        foreach (var (origin, transate) in request.Translations)
        {
            if(language != null)
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
