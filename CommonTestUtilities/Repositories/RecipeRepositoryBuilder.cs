using Moq;
using RevenuesBook.Domain.IRepositories;

namespace CommonTestUtilities.Repositories;
public class RecipeRepositoryBuilder
{
    private readonly Mock<IRecipeRepository> _recipeRepositoryMock;

    public RecipeRepositoryBuilder()
    {
        _recipeRepositoryMock = new Mock<IRecipeRepository>();
    }
    public IRecipeRepository Build()
    {
        return _recipeRepositoryMock.Object;
    }
}
