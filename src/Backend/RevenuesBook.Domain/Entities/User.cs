using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace RevenuesBook.Domain.Entities;

[Table("users")]
public class User
{
    [Key]
    [NotNull]
    public Guid Id { get; set; } = Guid.NewGuid();

    [StringLength(50)]
    [NotNull]
    public string Name { get; set; }

    [StringLength(50)]
    [EmailAddress]
    [NotNull]
    public string Email { get; set; }

    [StringLength(2000)]
    [NotNull]
    public string Password { get; set; }

    [NotNull]
    [DefaultValue(true)]
    public bool isActive { get; set; } = true;

    [NotNull]
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    [NotNull]
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

}
