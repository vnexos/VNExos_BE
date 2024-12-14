using Swashbuckle.AspNetCore.Annotations;
using VNExos.Common.Transferer;
using VNExos.Domain.Dtos;

namespace VNExos.Application.Languages.Queries.GetAllLanguages;

public class GetAllLanguagesQuery : CommonListTransferer<LanguageDto>
{
    [SwaggerIgnore]
    public override string? SearchPhase { get; set; }
}
