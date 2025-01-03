using AutoMapper;
using VNExos.Application.Translations.Dtos;
using VNExos.Common.Transferer;
using VNExos.Domain.Dtos;
using VNExos.Domain.Entities;
using VNExos.Domain.Repositories;

namespace VNExos.Application.Translations.Commands.CreateTranslationsByLanguageCode;

public class CreateTranslationsByLanguageCodeCommandHandler
        : ACommonListTransfererHandler<Translation, TranslationDto, CreateTranslationsByLanguageCodeCommand, ATranslationRepository>
{
    private readonly IMapper _mapper;
    private readonly ATranslationRepository _translationRepository;
    private readonly ALanguageRepository _languageRepository;

    public CreateTranslationsByLanguageCodeCommandHandler(IMapper mapper, ATranslationRepository repository, ALanguageRepository languageRepository) : base(mapper, repository)
    {
        _mapper = mapper;
        _languageRepository = languageRepository;
        _translationRepository = repository;
    }

    public async override Task<ICollection<TranslationDto>> Handle(CreateTranslationsByLanguageCodeCommand request, CancellationToken cancellationToken)
    {
        var code = request.LanguageCode;
        var language = await _languageRepository.GetByCode(code!);
        var failed = new List<Translation>();
        var translations = new List<Translation>();
        foreach (var (requestOrigin, requestTransate) in request.Translations)
        {
            if(language != null)
                translations.Add(new Translation { LanguageId = language!.Id, Origin = requestOrigin, Translate = requestTransate, CreatedAt = DateTime.UtcNow });
            else
                failed.Add(new Translation { LanguageId = Guid.Empty, Origin = requestOrigin, Translate = requestTransate });
        }
        return await CreateTranslations.CreateTranslation(_translationRepository, _mapper, translations, failed);
    }
}
