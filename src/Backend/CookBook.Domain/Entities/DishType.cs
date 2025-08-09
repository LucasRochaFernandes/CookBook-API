using System.ComponentModel.DataAnnotations.Schema;

namespace CookBook.Domain.Entities;

[Table("DishTypes")]
public class DishType
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Enums.DishType Type { get; set; }
    public Guid RecipeId { get; set; }
}
