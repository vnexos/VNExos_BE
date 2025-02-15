using Swashbuckle.AspNetCore.Annotations;
using VNExos.Common.Transferer;
using VNExos.Domain.Dtos;

namespace VNExos.Application.Translations.Commands.UpdateTranslationById;

public class UpdateTranslationByIdCommand : CommonTransferer<TranslationDto>
{
    [SwaggerIgnore]
    public override Guid Id { get; set; }

    public string? Origin { get; set; }
    public string? Translate { get; set; }
}
