using AutoMapper;
using VNExos.Common.Transferer;
using VNExos.Domain.Dtos;
using VNExos.Domain.Entities;
using VNExos.Domain.Repositories;

namespace VNExos.Application.Translations.Commands.UpdateTranslationByCodeAndOrigin;

public class UpdateTranslationByCodeAndOriginCommandHandler : ACommonTransfererHandler<Translation, TranslationDto, UpdateTranslationByCodeAndOriginCommand, ATranslationRepository>
{
    private readonly IMapper _mapper;
    private readonly ATranslationRepository _translationRepository;
    public UpdateTranslationByCodeAndOriginCommandHandler(IMapper mapper, ATranslationRepository repository) : base(mapper, repository)
    {
        _mapper = mapper;
        _translationRepository = repository;
    }

    public override async Task<TranslationDto> Handle(UpdateTranslationByCodeAndOriginCommand request, CancellationToken cancellationToken)
    {
        var res = await _translationRepository.UpdateTranslationsByCodeAndOrigin(request.LanguageCode!, request.TranslationOrigin!, _mapper.Map<Translation>(request));
        return _mapper.Map<TranslationDto>(res);
    }
}
