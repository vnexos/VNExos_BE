using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel;
using VNExos.Common.Transferer;
using VNExos.Domain.Dtos;

namespace VNExos.Application.Languages.Commands.CreateLanguage;

public class CreateLanguageCommand : CommonTransferer<LanguageDto>
{
    [SwaggerIgnore]
    public override Guid Id { get; set; }
    [DefaultValue("vi-VN")]
    public string? Code { get; set; }
    [DefaultValue("Tiếng Việt")]
    public string? Name { get; set; }
    [DefaultValue(null)]
    public string? FlagUrl { get; set; }
    public bool? IsDefault { get; set; } = false;
    public bool? RightToLeft { get; set; } = false;
    [DefaultValue(null)]
    public string? Description { get; set; }
}
