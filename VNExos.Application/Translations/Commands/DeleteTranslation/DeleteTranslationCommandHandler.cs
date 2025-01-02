using AutoMapper;
using VNExos.Common.Transferer;
using VNExos.Domain.Dtos;
using VNExos.Domain.Entities;
using VNExos.Domain.Repositories;

namespace VNExos.Application.Translations.Commands.DeleteTranslation;

public class DeleteTranslationCommandHandler(IMapper mapper, ATranslationRepository repository) : ACommonTransfererHandler<Translation, TranslationDto, DeleteTranslationCommand, ATranslationRepository>(mapper, repository)
{
    public override async Task<TranslationDto> Handle(DeleteTranslationCommand request, CancellationToken cancellationToken)
    {
        return await Delete(request.Id);
    }
}
