using AutoMapper;
using VNExos.Common.Transferer;
using VNExos.Domain.Dtos;
using VNExos.Domain.Entities;
using VNExos.Domain.Repositories;

namespace VNExos.Application.Languages.Commands.DeleteLanguage;

public class DeleteLanguageCommandHandler : ACommonTransfererHandler<Language, LanguageDto, DeleteLanguageCommand, ALanguageRepository>
{
    public DeleteLanguageCommandHandler(IMapper mapper, ALanguageRepository repository) : base(mapper, repository)
    {
    }

    public override async Task<LanguageDto> Handle(DeleteLanguageCommand request, CancellationToken cancellationToken)
    {
        return await Delete(request.Id);
    }
}
