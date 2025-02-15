using AutoMapper;
using VNExos.Common.Transferer;
using VNExos.Domain.Dtos;
using VNExos.Domain.Entities;
using VNExos.Domain.Repositories;

namespace VNExos.Application.Translations.Commands.UpdateTranslationById;

public class UpdateTranslationByIdCommandHandler : ACommonTransfererHandler<Translation, TranslationDto, UpdateTranslationByIdCommand, ATranslationRepository>
{
    public UpdateTranslationByIdCommandHandler(IMapper mapper, ATranslationRepository repository) : base(mapper, repository)
    {
    }

    public override async Task<TranslationDto> Handle(UpdateTranslationByIdCommand request, CancellationToken cancellationToken)
    {
        return await Update(request);
    }
}
