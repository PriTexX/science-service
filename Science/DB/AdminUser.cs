using System.ComponentModel.DataAnnotations;

namespace Science.DB;

public sealed class AdminUser
{
    [Key]
    [MaxLength(36)]
    public required string Guid { get; set; }

    public required string FullName { get; set; }
}
