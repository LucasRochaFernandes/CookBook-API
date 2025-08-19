using System.ComponentModel.DataAnnotations.Schema;

namespace CookBook.Domain.Entities;

[Table("RefreshTokens")]
public class RefreshToken
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public required string Value { get; set; } = string.Empty;
    public required Guid UserId { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public User User { get; set; } = default!;
}
