using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VNExos.Common.Transferer;
using VNExos.Domain.Dtos;
using VNExos.Domain.Entities;
using VNExos.Domain.Repositories;

namespace VNExos.Application.Languages.Queries.GetLanguageByCode;

public class GetLanguageByCodeQueryHandler(IMapper mapper, ALanguageRepository repository) : ACommonTransfererHandler<Language, LanguageDto, GetLanguageByCodeQuery, ALanguageRepository>(mapper, repository)
{
    public override async Task<LanguageDto> Handle(GetLanguageByCodeQuery request, CancellationToken cancellationToken)
    {
        var res = await repository.GetByCode(request.Code);
        return mapper.Map<LanguageDto>(res);
    }
}
