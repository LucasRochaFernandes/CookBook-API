using RevenuesBook.Domain.Dtos;
using RevenuesBook.Domain.Entities;

namespace RevenuesBook.Domain.IRepositories;
public interface IRecipeRepository
{
    public Task<Guid> Create(Recipe entityRecipe);
    public Task<IList<Recipe>> Filter(User user, FilterRecipeDto filtersDto);

    public Task<Recipe?> GetById(User user, Guid recipeId);
    public void Update(Recipe entityRecipe);
    public Task Commit();
}
