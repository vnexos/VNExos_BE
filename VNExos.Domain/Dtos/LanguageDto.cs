using System.ComponentModel.DataAnnotations;
using VNExos.Common.DataTransferObject;
using VNExos.Domain.Entities;

namespace VNExos.Domain.Dtos;

public class LanguageDto : CommonDto
{
    public string? Code { get; set; }
    public string? Name { get; set; }
    public string? FlagUrl { get; set; }
    public bool? IsDefault { get; set; }
    public bool? RightToLeft { get; set; }
    public string? Description { get; set; }

    public Dictionary<string, string> Translations { get; set; } = [];
}
