using CookBook.Domain.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace CookBook.Domain.Entities;

[Table("Recipes")]
public class Recipe
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Title { get; set; } = string.Empty;
    public Difficulty? Difficulty { get; set; }
    public CookingTime? CookingTime { get; set; }
    public IList<Ingredient> Ingredients { get; set; } = [];
    public IList<Instruction> Instructions { get; set; } = [];
    public IList<DishType> DishTypes { get; set; } = [];
    public Guid? UserId { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
