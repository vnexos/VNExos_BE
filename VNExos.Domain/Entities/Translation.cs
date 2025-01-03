using System.ComponentModel.DataAnnotations;
using VNExos.Common.Entity;

namespace VNExos.Domain.Entities;

public class Translation : CommonEntity
{
    [Required]
    public Guid LanguageId { get; set; }
    [Required]
    [MaxLength(256)]
    public string? Origin { get; set; }
    public string? Translate { get; set; }

    public Language? Language { get; set; }
}
