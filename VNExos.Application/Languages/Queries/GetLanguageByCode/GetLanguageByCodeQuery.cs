using VNExos.Common.Transferer;
using VNExos.Domain.Dtos;

namespace VNExos.Application.Languages.Queries.GetLanguageByCode;

public class GetLanguageByCodeQuery : CommonTransferer<LanguageDto>
{
    public string Code { get; set; }
}
