using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel;
using VNExos.Common.Transferer;
using VNExos.Domain.Dtos;

namespace VNExos.Application.Translations.Commands.CreateTranslationsByOrigin;

public class CreateTranslationsByOriginCommand : CommonListTransferer<TranslationDto>
{
    [SwaggerIgnore]
    public override string? SearchPhase { get; set; }

    [DefaultValue("DEFAUT_VALUE")]
    [SwaggerIgnore]
    public string? Origin { get; set; }
    public Dictionary<string, string> Languages { get; set; } = new Dictionary<string, string>()
    {
        { "vi-VN", "Giá trị mặc định" }
    };
}
