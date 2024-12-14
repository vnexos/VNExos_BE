using System.ComponentModel.DataAnnotations;
using VNExos.Common.Entity;

namespace VNExos.Domain.Entities;

public class Language : CommonEntity
{
    [Required]
    public required string Code { get; set; }
    [Required]
    public required string Name { get; set; }
    [Required]
    public required string FlagUrl { get; set; }
    [Required]
    public required bool RightToLeft { get; set; } = false;

    public string? Description { get; set; }
}
