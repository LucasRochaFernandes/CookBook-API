using RevenuesBook.Domain.Entities;
using System.Linq.Expressions;

namespace RevenuesBook.Domain.IRepositories;
public interface IRecipeRepository
{
    public Task<Guid> Create(Recipe entityRecipe);
    public Task<Recipe?> FindBy(Expression<Func<Recipe, bool>> condition, bool AsNoTracking = false);
    public void Update(Recipe entityRecipe);
    public Task Commit();
}
