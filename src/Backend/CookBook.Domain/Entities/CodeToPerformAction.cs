using System.ComponentModel.DataAnnotations.Schema;

namespace CookBook.Domain.Entities;

[Table("CodeToPerformActions")]
public class CodeToPerformAction
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public string Value { get; set; } = string.Empty;
    public Guid UserId { get; set; }
}
