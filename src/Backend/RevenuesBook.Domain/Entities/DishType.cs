namespace RevenuesBook.Domain.Entities;
public class DishType
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Enums.DishType Type { get; set; }
    public Guid RecipeId { get; set; }
}
