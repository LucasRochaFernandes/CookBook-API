using Bogus;
using CookBook.Domain.Dtos;
using CookBook.Domain.Entities;
using CookBook.Domain.IRepositories;
using Moq;

namespace CommonTestUtilities.Repositories;
public class RecipeRepositoryBuilder
{
    private readonly Mock<IRecipeRepository> _recipeRepositoryMock;

    public RecipeRepositoryBuilder()
    {
        _recipeRepositoryMock = new Mock<IRecipeRepository>();
    }
    public RecipeRepositoryBuilder Filter(User user, IList<Recipe> recipes)
    {
        _recipeRepositoryMock.Setup(rep => rep.Filter(user, It.IsAny<FilterRecipeDto>()))
            .ReturnsAsync(recipes);
        return this;
    }
    public RecipeRepositoryBuilder GetById(User user, Recipe? recipe)
    {
        if (recipe is not null)
        {
            _recipeRepositoryMock
                .Setup(repository => repository.GetById(user, recipe.Id, true, It.IsAny<CancellationToken>()))
                .ReturnsAsync(recipe);
        }

        return this;
    }
    public RecipeRepositoryBuilder GetForDashboard(User user, IList<Recipe> recipes)
    {
        _recipeRepositoryMock.Setup(recipeRepository => recipeRepository.GetForDashboard(user)).ReturnsAsync(recipes);
        return this;
    }

    public IRecipeRepository Build()
    {
        return _recipeRepositoryMock.Object;
    }
}
