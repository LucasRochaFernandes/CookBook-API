using System.ComponentModel.DataAnnotations.Schema;

namespace RevenuesBook.Domain.Entities;

[Table("Ingredients")]
public class Ingredient
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Item { get; set; } = string.Empty;
    public Guid RecipeId { get; set; }
}
