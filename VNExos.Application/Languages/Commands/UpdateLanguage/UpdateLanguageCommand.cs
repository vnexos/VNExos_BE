using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel;
using VNExos.Common.Transferer;
using VNExos.Domain.Dtos;

namespace VNExos.Application.Languages.Commands.UpdateLanguage;

public class UpdateLanguageCommand : CommonTransferer<LanguageDto>
{
    [SwaggerIgnore]
    public override Guid Id { get; set; }
    [DefaultValue("vi-VN")]
    public string? Code { get; set; }
    [DefaultValue("Tiếng Việt")]
    public string? Name { get; set; }
    [DefaultValue(null)]
    public string? FlagUrl { get; set; }
    public bool? IsDefault { get; set; } = null;
    public bool? RightToLeft { get; set; } = null;
    [DefaultValue(null)]
    public string? Description { get; set; }
}
