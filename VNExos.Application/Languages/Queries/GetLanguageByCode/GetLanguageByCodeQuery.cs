using VNExos.Common.Transferer;
using VNExos.Domain.Dtos;

namespace VNExos.Application.Languages.Queries.GetLanguageByCode;

public class GetLanguageByCodeQuery : CommonTransferer<LanguageDto>
{
    public required string Code { get; set; }
}
