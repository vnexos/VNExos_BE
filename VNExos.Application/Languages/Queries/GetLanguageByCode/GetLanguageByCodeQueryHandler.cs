﻿using AutoMapper;
using VNExos.Common.Transferer;
using VNExos.Domain.Dtos;
using VNExos.Domain.Entities;
using VNExos.Domain.Repositories;

namespace VNExos.Application.Languages.Queries.GetLanguageByCode;

public class GetLanguageByCodeQueryHandler
    : ACommonTransfererHandler<Language, LanguageDto, GetLanguageByCodeQuery, ALanguageRepository>
{
    private readonly ALanguageRepository _languageRepository;
    private readonly IMapper _mapper;
    public GetLanguageByCodeQueryHandler(IMapper mapper, ALanguageRepository repository) : base(mapper, repository)
    {
        _languageRepository = repository;
        _mapper = mapper;
    }

    public override async Task<LanguageDto> Handle(GetLanguageByCodeQuery request, CancellationToken cancellationToken)
    {
        var language = await _languageRepository.GetByCode(request.Code);
        var res = _mapper.Map<LanguageDto>(language);

        return res;
    }
}
