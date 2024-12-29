using System.ComponentModel.DataAnnotations;
using VNExos.Common.Entity;

namespace VNExos.Domain.Entities;

public class Language : CommonEntity
{
    [Required]
    [MaxLength(10)]
    public string? Code { get; set; }
    [Required]
    [MaxLength(100)]
    public string? Name { get; set; }
    [Required]
    public string? FlagUrl { get; set; }
    public bool? IsDefault { get; set; } = false;
    public bool? RightToLeft { get; set; } = false;
    public string? Description { get; set; }
}
