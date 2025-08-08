using Microsoft.EntityFrameworkCore;
using RevenuesBook.Domain.Entities;
using RevenuesBook.Domain.IRepositories;
using System.Linq.Expressions;

namespace RevenuesBook.Infra.Repositories;
public class RecipeRepository : IRecipeRepository
{
    private readonly AppDbContext _appDbContext;

    public RecipeRepository(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }

    public async Task Commit()
    {
        await _appDbContext.SaveChangesAsync();
    }

    public async Task<Guid> Create(Recipe entityRecipe)
    {
        var result = await _appDbContext.Recipes.AddAsync(entityRecipe);
        return result.Entity.Id;
    }

    public async Task<Recipe?> FindBy(Expression<Func<Recipe, bool>> condition, bool AsNoTracking = false)
    {
        if (AsNoTracking)
        {
            return await _appDbContext.Recipes.AsNoTracking().FirstOrDefaultAsync(condition);
        }
        else
        {
            return await _appDbContext.Recipes.FirstOrDefaultAsync(condition);
        }
    }

    public void Update(Recipe entityUser)
    {
        _appDbContext.Recipes.Update(entityUser);
    }
}
