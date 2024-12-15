using AutoMapper;
using VNExos.Common.Transferer;
using VNExos.Domain.Dtos;
using VNExos.Domain.Entities;
using VNExos.Domain.Repositories;

namespace VNExos.Application.Languages.Commands.CreateLanguage;

public class CreateLanguageCommandHandler(IMapper mapper, ALanguageRepository repository) 
    : ACommonTransfererHandler<Language, LanguageDto, CreateLanguageCommand, ALanguageRepository>(mapper, repository)
{
    public override async Task<LanguageDto> Handle(CreateLanguageCommand request, CancellationToken cancellationToken)
    {
        return await Create(request);
    }
}
