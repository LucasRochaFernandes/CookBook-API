using Moq;
using CookBook.Domain.Dtos;
using CookBook.Domain.Entities;
using CookBook.Domain.IRepositories;

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
    public IRecipeRepository Build()
    {
        return _recipeRepositoryMock.Object;
    }
}
