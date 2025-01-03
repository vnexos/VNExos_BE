using System.ComponentModel.DataAnnotations;
using VNExos.Common.DataTransferObject;

namespace VNExos.Domain.Dtos;

public class TranslationDto : CommonDto
{
    public Guid LanguageId { get; set; }
    public string? Origin { get; set; }
    public string? Translate { get; set; }
}
