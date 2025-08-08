using System.ComponentModel.DataAnnotations.Schema;

namespace RevenuesBook.Domain.Entities;

[Table("Instructions")]
public class Instruction
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public int Step { get; set; }
    public string Text { get; set; } = string.Empty;
    public Guid RecipeId { get; set; }
}
