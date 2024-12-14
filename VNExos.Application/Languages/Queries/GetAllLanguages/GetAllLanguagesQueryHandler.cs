using AutoMapper;
using VNExos.Common.Transferer;
using VNExos.Domain.Dtos;
using VNExos.Domain.Entities;
using VNExos.Domain.Repositories;

namespace VNExos.Application.Languages.Queries.GetAllLanguages;

public class GetAllLanguagesQueryHandler(IMapper mapper, ALanguageRepository repository)
    : ACommonListTransfererHandler<Language, LanguageDto, GetAllLanguagesQuery, ALanguageRepository>(mapper, repository)
{
    public override async Task<ICollection<LanguageDto>> Handle(GetAllLanguagesQuery request, CancellationToken cancellationToken)
    {
        return await GetAll();
    }
}
