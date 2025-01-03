using System.ComponentModel.DataAnnotations;

namespace VNExos.Common.Entity;

// Common entity
public class CommonEntity
{
    [Key]
    public Guid Id { get; set; }

    public DateTime? CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}
