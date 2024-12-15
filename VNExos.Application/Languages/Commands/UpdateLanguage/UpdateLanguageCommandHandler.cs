using AutoMapper;
using VNExos.Common.Transferer;
using VNExos.Domain.Dtos;
using VNExos.Domain.Entities;
using VNExos.Domain.Repositories;

namespace VNExos.Application.Languages.Commands.UpdateLanguage;

public class UpdateLanguageCommandHandler : ACommonTransfererHandler<Language, LanguageDto, UpdateLanguageCommand, ALanguageRepository>
{
    public UpdateLanguageCommandHandler(IMapper mapper, ALanguageRepository repository) : base(mapper, repository)
    {
    }

    public override async Task<LanguageDto> Handle(UpdateLanguageCommand request, CancellationToken cancellationToken)
    {
        return await Update(request);
    }
}
