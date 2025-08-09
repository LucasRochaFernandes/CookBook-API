using Microsoft.EntityFrameworkCore;
using CookBook.Domain.Dtos;
using CookBook.Domain.Entities;
using CookBook.Domain.IRepositories;

namespace CookBook.Infra.Repositories;
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

    public async Task<IList<Recipe>> Filter(User user, FilterRecipeDto filtersDto)
    {
        var query = _appDbContext
            .Recipes
            .AsNoTracking()
            .Include(recipe => recipe.Ingredients)
            .Where(recipe => recipe.UserId.Equals(user.Id));
        if (filtersDto.Difficulties.Any())
        {
            query = query.Where(recipe => recipe.Difficulty.HasValue && filtersDto.Difficulties.Contains(recipe.Difficulty.Value));
        }
        if (filtersDto.CookingTimes.Any())
        {
            query = query.Where(recipe => recipe.CookingTime.HasValue && filtersDto.CookingTimes.Contains(recipe.CookingTime.Value));
        }
        if (filtersDto.DishTypes.Any())
        {
            query = query.Where(recipe => recipe.DishTypes.Any(dishType => filtersDto.DishTypes.Contains(dishType.Type)));
        }
        if (!string.IsNullOrEmpty(filtersDto.RecipeTitle_Ingredient))
        {
            query = query.Where(recipe =>
                recipe.Title.Contains(filtersDto.RecipeTitle_Ingredient)
                    || recipe.Ingredients.Any(ingredient => ingredient.Item.Contains(filtersDto.RecipeTitle_Ingredient)));
        }

        return await query.ToListAsync();
    }

    public async Task<Recipe?> GetById(User user, Guid recipeId)
    {
        return await _appDbContext.Recipes
            .AsNoTracking()
            .Include(recipe => recipe.Instructions)
            .Include(recipe => recipe.DishTypes)
            .Include(recipe => recipe.Ingredients)
            .FirstOrDefaultAsync(recipe => recipe.UserId.Equals(user.Id) && recipe.Id.Equals(recipeId));
    }

    public void Update(Recipe entityUser)
    {
        _appDbContext.Recipes.Update(entityUser);
    }
}
