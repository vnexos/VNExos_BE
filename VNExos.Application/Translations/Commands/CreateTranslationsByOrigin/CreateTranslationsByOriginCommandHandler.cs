using AutoMapper;
using VNExos.Application.Translations.Dtos;
using VNExos.Common.Transferer;
using VNExos.Domain.Dtos;
using VNExos.Domain.Entities;
using VNExos.Domain.Repositories;

namespace VNExos.Application.Translations.Commands.CreateTranslationsByOrigin;

public class CreateTranslationsByOriginCommandHandler
    : ACommonListTransfererHandler<Translation, TranslationDto, CreateTranslationsByOriginCommand, ATranslationRepository>
{
    private readonly IMapper _mapper;
    private readonly ATranslationRepository _translationRepository;
    private readonly ALanguageRepository _languageRepository;

    public CreateTranslationsByOriginCommandHandler(IMapper mapper, ATranslationRepository repository, ALanguageRepository languageRepository) : base(mapper, repository)
    {
        _mapper = mapper;
        _translationRepository = repository;
        _languageRepository = languageRepository;
    }

    public override async Task<ICollection<TranslationDto>> Handle(CreateTranslationsByOriginCommand request, CancellationToken cancellationToken)
    {
        var origin = request.Origin;
        var languages = await _languageRepository.GetByCodeRange(request.Languages.Keys);
        var failed = new List<Translation>();
        var translations = new List<Translation>();
        foreach (var (requestCode, requestTransate) in request.Languages)
        {
            var language = languages.FirstOrDefault(r => r.Code == requestCode);
            if (language != null)
                translations.Add(new Translation { LanguageId = language!.Id, Origin = origin, Translate = requestTransate, CreatedAt = DateTime.UtcNow });
            else
                failed.Add(new Translation { LanguageId = Guid.Empty, Origin = origin, Translate = requestTransate });
        }
        return await CreateTranslations.CreateTranslation(_translationRepository, _mapper, translations, failed);
    }
}
