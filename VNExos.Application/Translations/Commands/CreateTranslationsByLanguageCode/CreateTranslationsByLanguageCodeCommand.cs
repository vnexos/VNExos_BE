using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel;
using VNExos.Common.Transferer;
using VNExos.Domain.Dtos;

namespace VNExos.Application.Translations.Commands.CreateTranslationsByLanguageCode;

public class CreateTranslationsByLanguageCodeCommand : CommonListTransferer<TranslationDto>
{
    [SwaggerIgnore]
    public override string? SearchPhase { get; set; }

    [DefaultValue("vi-VN")]
    [SwaggerIgnore]
    public string? LanguageCode { get; set; }
    
    public Dictionary<string, string> Translations { get; set; } = new Dictionary<string, string>()
    {
        { "DEFAULT_VALUE", "Giá trị mặc định" }
    };
}
