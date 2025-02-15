using Swashbuckle.AspNetCore.Annotations;
using VNExos.Common.Transferer;
using VNExos.Domain.Dtos;

namespace VNExos.Application.Translations.Commands.UpdateTranslationByCodeAndOrigin;

public class UpdateTranslationByCodeAndOriginCommand : CommonTransferer<TranslationDto>
{
    [SwaggerIgnore]
    public override Guid Id { get; set; }
    
    [SwaggerIgnore]
    public string? LanguageCode { get; set; }
    [SwaggerIgnore]
    public string? TranslationOrigin { get; set; }

    public string? Origin { get; set; }
    public string? Translate { get; set; }
}
